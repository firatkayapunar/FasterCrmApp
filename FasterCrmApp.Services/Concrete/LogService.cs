using AutoMapper;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Validation;
using FasterCrmApp.Services.Validation.FluentValidation;

namespace FasterCrmApp.Services.Concrete
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public LogService(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public Result<LogModel> GetById(int id)
        {
            try
            {
                var existingClient = _logRepository.GetById(id);

                if (existingClient == null)
                    return Result<LogModel>.FailureResult("Log not found.", new List<string> { "The log with the given ID does not exist." });

                var logModel = _mapper.Map<LogModel>(existingClient);

                return Result<LogModel>.SuccessResult(logModel, "Log retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<LogModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<LogModel>> GetList()
        {
            try
            {
                var logs = _logRepository.GetAll();

                if (logs == null || !logs.Any())
                    return Result<List<LogModel>>.SuccessResult(new List<LogModel>(), "No logs found.");

                var logModels = _mapper.Map<List<LogModel>>(logs);

                return Result<List<LogModel>>.SuccessResult(logModels.OrderByDescending(x => x.CreatedAt).ToList(), "Logs retrieved successfully.");

            }
            catch (Exception ex)
            {
                return Result<List<LogModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<LogModel>> GetListByUserId(int userId)
        {
            try
            {
                var existingClient = _logRepository.GetAll(x => x.UserID == userId);

                if (existingClient == null || !existingClient.Any())
                    return Result<List<LogModel>>.SuccessResult(new List<LogModel>(), "No logs found.");

                var logModels = _mapper.Map<List<LogModel>>(existingClient);

                return Result<List<LogModel>>.SuccessResult(logModels.OrderByDescending(x => x.CreatedAt).Take(10).ToList(), "Logs retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<LogModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result Create(CreateLogModel createLogModel)
        {
            try
            {
                var log = _mapper.Map<Log>(createLogModel);
                ValidationTool.Validate(new LogValidator(), log);

                _logRepository.Add(log);

                return Result.SuccessResult("Log successfully added.");
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

                return Result.FailureResult("An error occurred while adding the log.", errors);
            }
        }

        public Result Edit(EditLogModel editLogModel)
        {
            try
            {
                var existingLog = _logRepository.GetById(editLogModel.ID);

                if (existingLog == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The log with the given ID does not exist." } }
                    };

                    return Result.FailureResult("Log not found.", errors);
                }

                var log = _mapper.Map(editLogModel, existingLog);

                ValidationTool.Validate(new LogValidator(), log);

                _logRepository.Update(log);

                return Result.SuccessResult("Log successfully updated.");
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

                return Result.FailureResult("An error occurred while updating the log.", errors);
            }
        }

        public Result Delete(DeleteLogModel deleteLogModel)
        {
            try
            {
                var log = _logRepository.GetById(deleteLogModel.ID);

                if (log == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The log with the given ID does not exist." } }
                    };

                    return Result.FailureResult("Log not found.", errors);
                }

                _logRepository.Remove(log.ID);

                return Result.SuccessResult("Log successfully deleted.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };

                return Result.FailureResult("An error occurred while deleting the log.", errors);
            }
        }
    }
}
