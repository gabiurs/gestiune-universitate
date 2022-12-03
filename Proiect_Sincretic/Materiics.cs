using System;

public class Materii
{
    public string Nume { get; set; }
    public int? Nrcredite { get; set; }
    public int? Nrore { get; set; }
    public float? Suma { get; set; }
   
    public override string ToString()
    {
        return Nume;
    }

}