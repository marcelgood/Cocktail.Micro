using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Cocktail.Utils;
using IdeaBlade.EntityModel;
using IdeaBlade.Validation;

namespace DomainModel
{
    [DataContract(IsReference = true)]
    public class Rate : EntityBase, IHasRoot
    {
        internal Rate()
        {
        }

        /// <summary>Gets or sets the Id. </summary>
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RequiredValueVerifier(ErrorMessageResourceName = "Rate_Id")]
        public Guid Id { get; internal set; }

        /// <summary>Gets or sets the Amount. </summary>
        [DataMember]
        [RequiredValueVerifier(ErrorMessageResourceName = "Rate_Amount")]
        public decimal Amount { get; set; }

        /// <summary>Gets or sets the RateTypeId. </summary>
        [DataMember]
        [ForeignKey("RateType")]
        [RequiredValueVerifier(ErrorMessageResourceName = "Rate_RateTypeId")]
        public Guid RateTypeId { get; set; }

        /// <summary>Gets or sets the ResourceId. </summary>
        [DataMember]
        [ForeignKey("Resource")]
        [RequiredValueVerifier(ErrorMessageResourceName = "Rate_ResourceId")]
        public Guid ResourceId { get; set; }

        /// <summary>Gets or sets the RateType. </summary>
        [DataMember]
        [InverseProperty("Rates")]
        public RateType RateType { get; set; }

        /// <summary>Gets or sets the Resource. </summary>
        [DataMember]
        [InverseProperty("Rates")]
        public Resource Resource { get; set; }

        #region IHasRoot Members

        public object Root
        {
            get { return !EntityAspect.Wrap(Resource).IsNullEntity ? Resource : null; }
        }

        #endregion

        internal static Rate Create(RateType type)
        {
            return new Rate { Id = CombGuid.NewGuid(), RateTypeId = type.Id };
        }
    }
}