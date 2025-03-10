using System.ComponentModel.DataAnnotations;
namespace MyCar.Common.Model
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
            DataInsert = DateTime.Now;
            IsDeleted = false;
        }

        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Usuario que criou o registro
        /// </summary>
        public Guid CadUsserInsertId { get; set; }
        /// <summary>
        /// Usuario que modificou resgistro
        /// </summary>
        public Guid CadUsserUpdateId { get; set; }
        /// <summary>
        /// Regista data/hora que o registro foi inserido.
        /// </summary>
        public DateTime DataInsert { get; set; }
        /// <summary>
        /// Regista data/hora do registro atualizado.
        /// </summary>
        public DateTime? DataUpdate { get; private set; }
        /// <summary>
        /// Regista data/hora que o registro foi excluido.
        /// </summary>
        public DateTime? DataExclusao { get; private set; }
        /// <summary>
        /// Marca o registro como deletado.
        /// </summary>
        public bool? IsDeleted { get; set; }

        #region Metodos 

        public void Updat(DateTime? dataUpdate = null)
        {
            DataUpdate = dataUpdate ?? DateTime.Now;
        }

        /// <summary>
        /// Marca o registro como deletado
        /// </summary>
        public void DeleteUpdate()
        {
            DataExclusao = DateTime.Now;
            IsDeleted = true;
        }

        public void Delete() => DataExclusao = DateTime.Now;

        #endregion
    }
}
