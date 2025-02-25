using AutoMapper;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Entities.Enum;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Validation;
using FasterCrmApp.Services.Validation.FluentValidation;

namespace FasterCrmApp.Services.Concrete
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public IssueService(IIssueRepository issueRepository, INotificationService notificationService, IMapper mapper)
        {
            _issueRepository = issueRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public Result<IssueModel> GetById(int id)
        {
            try
            {
                var existingClient = _issueRepository.GetById(id);

                if (existingClient == null)
                    return Result<IssueModel>.FailureResult("Issue not found.", new List<string> { "The issue with the given ID does not exist." });

                var issueModel = _mapper.Map<IssueModel>(existingClient);

                return Result<IssueModel>.SuccessResult(issueModel, "Issue retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<IssueModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<IssueModel>> GetList()
        {
            try
            {
                var issues = _issueRepository.GetAll();

                if (issues == null || !issues.Any())
                    return Result<List<IssueModel>>.SuccessResult(new List<IssueModel>(), "No issues found.");

                var issueModels = _mapper.Map<List<IssueModel>>(issues);

                return Result<List<IssueModel>>.SuccessResult(
                    issueModels.OrderBy(x => x.IsCompleted).ThenByDescending(x => x.CreatedAt).ToList(),
                    "Issues retrieved successfully."
                );

            }
            catch (Exception ex)
            {
                return Result<List<IssueModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<IssueModel>> GetListByUserId(int userId)
        {
            try
            {
                var existingClient = _issueRepository.GetAll(x => x.UserID == userId);

                if (existingClient == null || !existingClient.Any())
                    return Result<List<IssueModel>>.SuccessResult(new List<IssueModel>(), "No issues found.");

                var issueModels = _mapper.Map<List<IssueModel>>(existingClient);

                return Result<List<IssueModel>>.SuccessResult(
                    issueModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Issues retrieved successfully."
                );
            }
            catch (Exception ex)
            {
                return Result<List<IssueModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<IssueModel>> ListBySearch(string search)
        {
            try
            {
                var issues = _issueRepository.GetAll(x =>
                             x.Summary.Contains(search)
                );

                // Eğer veri yoksa hata yerine boş bir liste dönelim.
                if (issues == null || !issues.Any())
                    return Result<List<IssueModel>>.SuccessResult(new List<IssueModel>(), "No issues found.");

                var issueModels = _mapper.Map<List<IssueModel>>(issues);

                return Result<List<IssueModel>>.SuccessResult(
                    issueModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Issues retrieved successfully."
                );

            }
            catch (Exception ex)
            {
                return Result<List<IssueModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<IssueModel>> ListBySearch(string search, int userId)
        {
            try
            {
                var issues = _issueRepository.GetAll(x =>
                             x.UserID == userId
                             &&
                             x.Summary.Contains(search)
                );

                if (issues == null || !issues.Any())
                    return Result<List<IssueModel>>.SuccessResult(new List<IssueModel>(), "No issues found.");

                var issueModels = _mapper.Map<List<IssueModel>>(issues);

                return Result<List<IssueModel>>.SuccessResult(
                    issueModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Issues retrieved successfully."
                );

            }
            catch (Exception ex)
            {
                return Result<List<IssueModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result Create(CreateIssueModel createIssueModel)
        {
            try
            {
                var issue = _mapper.Map<Issue>(createIssueModel);
                ValidationTool.Validate(new IssueValidator(), issue);

                _issueRepository.Add(issue);

                // Aslında burada Unit of Work pattern kullanarak, Issue ekleme işlemi sırasında herhangi bir sorun yaşanmazken, Notification eklenirken bir hata oluştuğu durumu kontrol altına almamız ve işlemi transaction ile yönetmemiz gerekirdi. Böylece, herhangi bir aşamada hata meydana geldiğinde tüm işlemler geri alınarak (rollback) veri bütünlüğü korunmuş olur.

                var notificationResult = _notificationService.Create(new CreateNotificationModel
                {
                    Text = "Yeni görev atandı.",
                    NotifcationType = (int)NotificationType.IssueAdded,
                    UserID = createIssueModel.UserID
                });

                return Result.SuccessResult("Issue successfully added.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while adding the issue.", errors);
            }
        }

        public Result Edit(EditIssueModel editIssueModel)
        {
            try
            {
                var existingIssue = _issueRepository.GetById(editIssueModel.ID);

                if (existingIssue == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The issue with the given ID does not exist." } }
                    };
                    return Result.FailureResult("Issue not found.", errors);
                }

                var issue = _mapper.Map(editIssueModel, existingIssue);

                ValidationTool.Validate(new IssueValidator(), issue);

                _issueRepository.Update(issue);

                // Aslında burada Unit of Work pattern kullanarak, Issue güncelleme işlemi sırasında herhangi bir sorun yaşanmazken, Notification eklenirken bir hata oluştuğu durumu kontrol altına almamız ve işlemi transaction ile yönetmemiz gerekirdi. Böylece, herhangi bir aşamada hata meydana geldiğinde tüm işlemler geri alınarak (rollback) veri bütünlüğü korunmuş olur.

                if (editIssueModel.IsCompleted)
                {
                    var result = _notificationService.Create(new CreateNotificationModel
                    {
                        Text = "Görev tamamlandı.",
                        NotifcationType = (int)NotificationType.IssueCompleted,
                        UserID = editIssueModel.UserID
                    }
                    );
                }
                else
                {
                    var result = _notificationService.Create(new CreateNotificationModel
                    {
                        Text = "Görev güncellendi.",
                        NotifcationType = (int)NotificationType.IssueChanged,
                        UserID = editIssueModel.UserID
                    }
                    );
                }

                return Result.SuccessResult("Issue successfully updated.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while updating the issue.", errors);
            }
        }

        public Result Delete(DeleteIssueModel deleteIssueModel)
        {
            try
            {
                var issue = _issueRepository.GetById(deleteIssueModel.ID);

                if (issue == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The issue with the given ID does not exist." } }
                    };
                    return Result.FailureResult("Issue not found.", errors);
                }

                _issueRepository.Remove(issue.ID);

                return Result.SuccessResult("Issue successfully deleted.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while deleting the issue.", errors);
            }
        }
    }
}
