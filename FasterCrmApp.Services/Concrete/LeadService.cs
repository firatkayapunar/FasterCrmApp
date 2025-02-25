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
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public LeadService(ILeadRepository leadRepository, INotificationService notificationService, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public Result<LeadModel> GetById(int id)
        {
            try
            {
                var existingClient = _leadRepository.GetById(id);

                if (existingClient == null)
                    return Result<LeadModel>.FailureResult("Lead not found.", new List<string> { "The lead with the given ID does not exist." });

                var leadModel = _mapper.Map<LeadModel>(existingClient);

                return Result<LeadModel>.SuccessResult(leadModel, "Lead retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<LeadModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<LeadModel>> GetList()
        {
            try
            {
                var leads = _leadRepository.GetAll();

                if (leads == null || !leads.Any())
                    return Result<List<LeadModel>>.SuccessResult(new List<LeadModel>(), "No leads found.");

                var leadModels = _mapper.Map<List<LeadModel>>(leads);

                foreach (var leadModel in leadModels)
                {
                    leadModel.LeadTypeName = LeadTypeHelper.GetLeadTypeName((LeadType)leadModel.LeadType);
                }

                return Result<List<LeadModel>>.SuccessResult(
                    leadModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Leads retrieved successfully."
                );

            }
            catch (Exception ex)
            {
                return Result<List<LeadModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<LeadModel>> GetListByUserId(int userId)
        {
            try
            {
                var existingClient = _leadRepository.GetAll(x => x.UserID == userId);

                if (existingClient == null || !existingClient.Any())
                    return Result<List<LeadModel>>.SuccessResult(new List<LeadModel>(), "No leads found.");

                var leadModels = _mapper.Map<List<LeadModel>>(existingClient);

                foreach (var leadModel in leadModels)
                {
                    leadModel.LeadTypeName = LeadTypeHelper.GetLeadTypeName((LeadType)leadModel.LeadType);
                }

                return Result<List<LeadModel>>.SuccessResult(
                    leadModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Leads retrieved successfully."
                );
            }
            catch (Exception ex)
            {
                return Result<List<LeadModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<LeadModel>> ListBySearch(string search)
        {
            try
            {
                var leads = _leadRepository.GetAll(x =>
                             x.Summary.Contains(search)
                );

                // Eğer veri yoksa hata yerine boş bir liste dönelim.
                if (leads == null || !leads.Any())
                    return Result<List<LeadModel>>.SuccessResult(new List<LeadModel>(), "No leads found.");

                var leadModels = _mapper.Map<List<LeadModel>>(leads);

                return Result<List<LeadModel>>.SuccessResult(
                    leadModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Leads retrieved successfully."
                );

            }
            catch (Exception ex)
            {
                return Result<List<LeadModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<LeadModel>> ListBySearch(string search, int userId)
        {
            try
            {
                var leads = _leadRepository.GetAll(x =>
                             x.UserID == userId
                             &&
                             x.Summary.Contains(search)
                );

                if (leads == null || !leads.Any())
                    return Result<List<LeadModel>>.SuccessResult(new List<LeadModel>(), "No leads found.");

                var leadModels = _mapper.Map<List<LeadModel>>(leads);

                return Result<List<LeadModel>>.SuccessResult(
                    leadModels.OrderByDescending(x => x.CreatedAt).ToList(),
                    "Leads retrieved successfully."
                );

            }
            catch (Exception ex)
            {
                return Result<List<LeadModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result Create(CreateLeadModel createLeadModel)
        {
            try
            {
                var lead = _mapper.Map<Lead>(createLeadModel);
                ValidationTool.Validate(new LeadValidator(), lead);

                _leadRepository.Add(lead);

                // Aslında burada Unit of Work pattern kullanarak, Lead ekleme işlemi sırasında herhangi bir sorun yaşanmazken, Notification eklenirken bir hata oluştuğu durumu kontrol altına almamız ve işlemi transaction ile yönetmemiz gerekirdi. Böylece, herhangi bir aşamada hata meydana geldiğinde tüm işlemler geri alınarak (rollback) veri bütünlüğü korunmuş olur.

                var notificationResult = _notificationService.Create(new CreateNotificationModel
                {
                    Text = "Yeni görev atandı.",
                    NotifcationType = (int)NotificationType.LeadAdded,
                    UserID = createLeadModel.UserID ?? 0
                });

                return Result.SuccessResult("Lead successfully added.");
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
                return Result.FailureResult("An error occurred while adding the lead.", errors);
            }
        }

        public Result Edit(EditLeadModel editLeadModel)
        {
            try
            {
                var existingLead = _leadRepository.GetById(editLeadModel.ID);

                if (existingLead == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The lead with the given ID does not exist." } }
                    };
                    return Result.FailureResult("Lead not found.", errors);
                }

                var lead = _mapper.Map(editLeadModel, existingLead);
                lead.ModifiedAt = DateTime.Now;

                ValidationTool.Validate(new LeadValidator(), lead);

                _leadRepository.Update(lead);

                // Aslında burada Unit of Work pattern kullanarak, Lead güncelleme işlemi sırasında herhangi bir sorun yaşanmazken, Notification eklenirken bir hata oluştuğu durumu kontrol altına almamız ve işlemi transaction ile yönetmemiz gerekirdi. Böylece, herhangi bir aşamada hata meydana geldiğinde tüm işlemler geri alınarak (rollback) veri bütünlüğü korunmuş olur.

                if (editLeadModel.LeadType == (int)LeadType.Completed)
                {
                    var result = _notificationService.Create(new CreateNotificationModel
                    {
                        Text = "Talep tamamlandı.",
                        NotifcationType = (int)NotificationType.LeadCompleted,
                        UserID = editLeadModel.UserID
                    }
                    );
                }
                else
                {
                    var result = _notificationService.Create(new CreateNotificationModel
                    {
                        Text = "Talep güncellendi.",
                        NotifcationType = (int)NotificationType.LeadChanged,
                        UserID = editLeadModel.UserID
                    }
                    );
                }

                return Result.SuccessResult("Lead successfully updated.");
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
                return Result.FailureResult("An error occurred while updating the lead.", errors);
            }
        }

        public Result Delete(DeleteLeadModel deleteLeadModel)
        {
            try
            {
                var lead = _leadRepository.GetById(deleteLeadModel.ID);

                if (lead == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The lead with the given ID does not exist." } }
                    };
                    return Result.FailureResult("Lead not found.", errors);
                }

                _leadRepository.Remove(lead.ID);

                return Result.SuccessResult("Lead successfully deleted.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while deleting the lead.", errors);
            }
        }
    }
}
