public interface IBackgroundJobsService
{
    Task SendEmail(string email, string subject);
}