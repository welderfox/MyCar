using MongoDB.Bson.Serialization.Attributes;

namespace MyCar.Models
{
    public class Car
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string TipoCombustivel { get; set; }
        public int Hodometro { get; set; }
        public bool Eletrico { get; set; }

        public Car(string marca, string modelo, string anoFabricacao, string tipoCombustivel, bool eletrico)
        {
            Marca = marca;
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            TipoCombustivel = tipoCombustivel;
            Eletrico = eletrico;
        }

        public void Update(string marca, string modelo, string anoFabricacao, string tipoCombustivel, bool eletrico, int hodometro)
        {
            Marca = marca;
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            TipoCombustivel = tipoCombustivel;
            Eletrico = eletrico;
            Hodometro = hodometro;
        }
    }
}
