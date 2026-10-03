Console.WriteLine("--- CÁLCULO DE JUROS ---");

Console.WriteLine("\nDigite o valor:");
decimal valor = decimal.Parse(Console.ReadLine()!);

Console.WriteLine("Digite a data de vencimento (dd/MM/yyyy):");
DateTime dataVencimento = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);
DateTime dataAtual = DateTime.Today;

int diasEmAtraso = (dataAtual - dataVencimento).Days;

if (diasEmAtraso <= 0) {
    Console.WriteLine("O pagamento não possui juros.");
    return;
}

decimal taxaJuros = 0.025m;
decimal juros = valor * taxaJuros * diasEmAtraso;
decimal valorTotal = valor + juros;

Console.WriteLine("\n--- RESULTADO ---");
Console.WriteLine($"Dias em atraso: {diasEmAtraso}");
Console.WriteLine($"Valor original: R$ {valor:F2}");
Console.WriteLine($"Juros: R$ {juros:F2}");
Console.WriteLine($"Valor total: R$ {valorTotal:F2}");
