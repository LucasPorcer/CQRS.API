using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Domain.Entities.Order;
using Orders.API.Domain.Interfaces.Services;
using Orders.API.Domain.Queries;
using Orders.API.Infra.Services.Commands.Order.Command;
using Orders.API.UI.BaseController;
using Orders.API.UI.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Orders.API.UI.Controllers
{
    [Route("api/EcomOrders")]
    [ApiController]
    public class EcomOrderController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IEcomOrderService _ecomOrderService;

        public EcomOrderController(IMediator mediator, IEcomOrderService ecomOrderService)
        {
            _mediator = mediator;
            _ecomOrderService = ecomOrderService;
        }

        /// <summary>
        /// Add order.
        /// </summary>
        [HttpPost(Name = "AddEcomOrder")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<HttpResponse>> AddEcomOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return result.DomainNotification.IsValid
                ? ApiResponse(HttpStatusCode.Created)
                : ApiResponse(HttpStatusCode.BadRequest, errors: result.DomainNotification.Errors);
        }

        /// <summary>
        /// Remove an order.
        /// </summary>
        [HttpDelete(Name = "RemoveEcomOrder")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<HttpResponse>> RemoveEcomOrder([FromBody] RemoveOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return result.DomainNotification.IsValid
                ? ApiResponse(HttpStatusCode.OK)
                : ApiResponse(HttpStatusCode.BadRequest, errors: result.DomainNotification.Errors);
        }

        /// <summary>
        /// Update an order.
        /// </summary>
        [HttpPatch(Name = "UpdateOrderStatus")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<HttpResponse>> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return result.DomainNotification.IsValid
                ? ApiResponse(HttpStatusCode.OK)
                : ApiResponse(HttpStatusCode.BadRequest, errors: result.DomainNotification.Errors);
        }

        [HttpPost(Name = "GetOrdersByFilter")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(HttpBodyResponse<IList<EcomOrder>>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<HttpResponse>> GetOrdersByFilter([FromBody] GetOrdersFilter query)
        {
            var result = _ecomOrderService.GetOrdersByFilter(query);

            if (result.Any())
                return ApiResponse(HttpStatusCode.OK, result);

            return ApiResponse(HttpStatusCode.NoContent);
        }
    }
}
