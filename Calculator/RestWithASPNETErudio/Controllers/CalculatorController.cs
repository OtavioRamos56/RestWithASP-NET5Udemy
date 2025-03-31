using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETErudio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")] //Onde o usuário irá definir os números
        public IActionResult Get(string firstNumber, string secondNumber)
        {
                //verifica se os numeros são validos
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {   //ConvertToDecimal é para os numeros serem convertidos para decimal e vai ser realziado a soma das variáveis
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                //Se tudo der certo retorna "Ok"
                return Ok(sum.ToString());
            }
            //retorna erro caso os numeros não sejam validos
            return BadRequest("Invalid Input");
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0; 
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber, 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isNumber;
            throw new NotImplementedException();
        }
    }
}
