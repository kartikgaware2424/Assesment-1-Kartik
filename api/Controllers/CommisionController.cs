using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    // DTOs for request and response
    public class CommissionCalculationRequest
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }

    public class CommissionCalculationResponse
    {
        public decimal AvalphaLocalCommission { get; set; }
        public decimal AvalphaForeignCommission { get; set; }
        public decimal AvalphaTotalCommission { get; set; }

        public decimal CompetitorLocalCommission { get; set; }
        public decimal CompetitorForeignCommission { get; set; }
        public decimal CompetitorTotalCommission { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class CommissionController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        public IActionResult Calculate(CommissionCalculationRequest request)
        {
            // This is Input validation
            if (request.LocalSalesCount < 0 || request.ForeignSalesCount < 0 || request.AverageSaleAmount < 0)
                return BadRequest("Sales counts and average amount must be ≥ 0.");

            // Avalpha commission
            decimal avalphaLocal = 0.2m * request.LocalSalesCount * request.AverageSaleAmount;
            decimal avalphaForeign = 0.35m * request.ForeignSalesCount * request.AverageSaleAmount;
            decimal avalphaTotal = avalphaLocal + avalphaForeign;

            // Competitor commission
            decimal competitorLocal = 0.02m * request.LocalSalesCount * request.AverageSaleAmount;
            decimal competitorForeign = 0.0755m * request.ForeignSalesCount * request.AverageSaleAmount;
            decimal competitorTotal = competitorLocal + competitorForeign;

            var response = new CommissionCalculationResponse
            {
                AvalphaLocalCommission = avalphaLocal,
                AvalphaForeignCommission = avalphaForeign,
                AvalphaTotalCommission = avalphaTotal,
                CompetitorLocalCommission = competitorLocal,
                CompetitorForeignCommission = competitorForeign,
                CompetitorTotalCommission = competitorTotal
            };

            return Ok(response);
        }
    }
}
