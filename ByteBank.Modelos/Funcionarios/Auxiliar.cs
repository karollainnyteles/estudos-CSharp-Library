using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Funcionarios
{
    public class Auxiliar : Funcionario
    {

        public Auxiliar(string cpf) : base(2000, cpf)
        {
            Console.WriteLine("criando Auxiliar");
        }

        public override void AumentarSalario()
        {
            Salario *= 1.1;
        }
        internal protected override double GetBonificacao()
        {
            return Salario * 0.2;
        }
    }
}
