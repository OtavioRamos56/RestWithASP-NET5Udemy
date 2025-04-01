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
        public IActionResult sum(string firstNumber, string secondNumber)
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
        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public IActionResult sub(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }
            return BadRequest("Invalid Input");
        }
        [HttpGet("mul/{firstNumber}/{secondNumber}")] 
        public IActionResult mul(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mul = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(mul.ToString());
            }
            return BadRequest("Invalid Input");
        }
        [HttpGet("div/{firstNumber}/{secondNumber}")]
        public IActionResult div(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(div.ToString());
            }
            return BadRequest("Invalid Input");
        }
        [HttpGet("med/{firstNumber}/{secondNumber}")]
        public IActionResult med(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var med = (ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber)) / 2;
                return Ok(med.ToString());
            }
            return BadRequest("Invalid Input");
        }
        [HttpGet("square/{firstNumber}")]
        public IActionResult square(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var square = Math.Sqrt((double)ConvertToDecimal(firstNumber));
                return Ok(square.ToString());
            }
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
