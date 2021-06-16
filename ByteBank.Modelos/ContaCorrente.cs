//using _05_ByteBank;

using _05_ByteBank;
using System;

namespace ByteBank
{
    /// <summary>
    /// Define uma conta corrente do banco ByteBank.
    /// </summary>
    public class ContaCorrente
    {
        public int contadorSaquesNaoPermitidos { get; private set; }
        public int ContadorTransferenciasNaoPermitidas { get; private set; }
        public double TaxaOperacao { get; private set; }
        public static int TotalContasCriadas { get; private set; }

        //cria uma propriedade com getter e setter
        public Cliente Titular { get; set; }
        public int Agencia { get; }
        public int Numero { get; }

        private double _saldo = 100;


        public double Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }

        /// <summary>
        /// Cria uma instência de Conta Corrente om os argumentos utilizados.
        /// </summary>
        /// <param name="agencia">Representa o valor da propriedade <see cref="Agencia"/> e deve possuir um valor maior que zero</param>
        /// <param name="numero">Representa o valor da propriedade <see cref="Numero"/> e deve possuir um valor maior que zero.</param>
        public ContaCorrente(int agencia, int numero)
        {
            if(agencia <=0)
            {
              throw new ArgumentException("A agencia deve ser maior que 0",nameof(agencia));
                
            }

            if (numero <= 0)
            {
               throw new ArgumentException("O numero deve ser maior que 0",nameof(numero));
              
            }


            Agencia = agencia;
            Numero = numero;

            TotalContasCriadas++;

            TaxaOperacao = 30 / TotalContasCriadas;
        }

        /// <summary>
        /// Realiza o saque e atualiza o valor da propriedade <see cref="Saldo"/>
        /// </summary>
        /// <exception cref="ArgumentException">Exeção lançada quando um valor negativo é utilizado no argumento valor</exception>
        /// <exception cref="SaldoInsuficienteException">Exceção lançada quando um valor negativo é utilizado no argumento <paramref name="valor"/>. </exception>
        /// <param name="valor">Representa o valor do saque, deve ser maior que 0 e menor que <see cref="Saldo"/>.</param>

        public void  Sacar(double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para o saque", nameof(valor));
            }
            if (_saldo < valor)
            {
                contadorSaquesNaoPermitidos++;

                throw new SaldoInsuficienteException(Saldo,valor);
               
            }

            _saldo -= valor;
            
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
        }

        public void Transferir(double valor, ContaCorrente contaDestino)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para a transferência", nameof(valor));
            }
            try
            {
                Sacar(valor);
            }
            catch (SaldoInsuficienteException e)
            {
                ContadorTransferenciasNaoPermitidas++;
                throw new OperacaoFinanceiraException("Operação não realizada.",e);
            }


            
            contaDestino.Depositar(valor);
           

        }
    }
}
