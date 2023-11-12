using System.Net;
using ClassManager.Business.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly INotificationServce _notificationService;

    protected BaseController(INotificationServce notificationService)
    {
        _notificationService = notificationService;
    }

    protected IActionResult CustomResponse(object? data)
    {
        if (_notificationService.TemNotificacoes())
            return BadRequest(new CustomProblemDetails(
                (int)HttpStatusCode.BadRequest,
                _notificationService.ObterNotificacoes().Select(p => p.Mensagem)));

        return Ok(data);
    }
}

public class CustomProblemDetails : ProblemDetails
{
    public IEnumerable<string> Errors { get; set; }
    public CustomProblemDetails(int statusCode, IEnumerable<string> errors)
    {
        Status = statusCode;
        Title = errors.Count() > 1 ? "Multiplos erros aconteceram" : "Um erro aconteceu";
        Errors = errors;
        Type = @"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
    }
}