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

        [HttpGet("sum/{firstNumber}/{secondNumber}")] //Onde o usu�rio ir� definir os n�meros
        public IActionResult Get(string firstNumber, string secondNumber)
        {
                //verifica se os numeros s�o validos
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {   //ConvertToDecimal � para os numeros serem convertidos para decimal e vai ser realziado a soma das vari�veis
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                //Se tudo der certo retorna "Ok"
                return Ok(sum.ToString());
            }
            //retorna erro caso os numeros n�o sejam validos
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
