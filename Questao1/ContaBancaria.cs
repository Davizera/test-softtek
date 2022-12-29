using System.Globalization;

namespace Questao1;

public class ContaBancaria
{
    private const double TaxaSaque = 3.5;
    public int NumeroConta { get; }
    public string Titular { get; set; }
    public double Saldo { get; private set; }
    public ContaBancaria(int numeroConta, string titular, double saldoInicial = 0)
    {
        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldoInicial;
    }

    public void Saque(double quantia)
    {
        Saldo -= (quantia + TaxaSaque);
    }

    public void Deposito(double quantia)
    {
        Saldo += quantia;
    }

    public override string ToString()
    {
        return $"Conta {NumeroConta}, Titular: {Titular}, Saldo: $ {Saldo}";
    }
}