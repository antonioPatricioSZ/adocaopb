using System.Net;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdocaoPB.Api.Filters;

public class ExceptionsFilter : IExceptionFilter {

    public void OnException(ExceptionContext context) {
        if(context.Exception is AdocaoPBException ) {
            TratarAdocaoPBException(context);
        } else {
            LancarErroDesconhecido(context);
        }
    }


    private static void TratarAdocaoPBException(ExceptionContext context) {

        if (context.Exception is ValidationErrorsException) {
            TratarErrosDeValidacaoException(context);
        } else if (context.Exception is InvalidLoginException) {
            TratarLoginException(context);
        } else if (context.Exception is NotFoundException) {
            TratarNotFounException(context);
        } else if (context.Exception is ForbidenException) {
            TratarUnaunthorizedException(context);
        }
        

    }


    private static void TratarErrosDeValidacaoException(ExceptionContext context) {
        var errosDeValidacaoException = context.Exception as ValidationErrorsException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(
            new JsonErrorResponse(errosDeValidacaoException.ErrorMessages)
        );
    }

    private static void TratarLoginException(ExceptionContext context) {
        var loginInvalidoException = context.Exception as InvalidLoginException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(
            new JsonErrorResponse(loginInvalidoException.Message)
        );
    }

    private static void TratarNotFounException(ExceptionContext context) {
        var notFoundException = context.Exception as NotFoundException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.Result = new ObjectResult(
            new JsonErrorResponse(notFoundException.Message)
        );
    }

    
    private static void TratarUnaunthorizedException(ExceptionContext context) {
        var notFoundException = context.Exception as ForbidenException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        context.Result = new ObjectResult(
            new JsonErrorResponse(notFoundException.Message)
        );
    }


    private static void LancarErroDesconhecido(ExceptionContext context) {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(
            new JsonErrorResponse(ResourceErrorMessages.ERRO_DESCONHECIDO)    
        );
    }

}
