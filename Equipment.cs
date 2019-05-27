
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace fyp.Models
{
    public class Equipment
    {
        public int EQUIPMENT_ID { get; set; }

        [Required(ErrorMessage = "Please enter valid Material No. ")]
        [Range(5, 6, ErrorMessage = "Material no. is 5-6 numbers")]
        public DateTime ELEMENT_MATERIAL_NO { get; set; }

        [Required(ErrorMessage = "Please enter Serial No.")]
        [Range(6, 7, ErrorMessage = "Serial no. is 6-7 numbers")]
        public double SERIAL_NO { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string EQUIPMENT_TYPE_ID { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string STORAGE_LOCATION { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string STORAGE_BIN { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string BOX_LOT_NO { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string QUANTITY { get; set; }

        [Required(ErrorMessage = "Please enter Date/Time")]
        [DataType(DataType.DateTime)]
        [Remote(action: "VerifyDate", controller: "Equipment")]
        public DateTime DTE_TIME_CR { get; set; }

        [Required(ErrorMessage = "Please enter Date/Time")]
        [DataType(DataType.DateTime)]
        [Remote(action: "VerifyDate", controller: "Equipment")]
        public DateTime DTE_TIME_LAST_MOD { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(20, ErrorMessage = "Max 20 chars")]
        public string CREATED_BY { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(20, ErrorMessage = "Max 20 chars")]
        public string MODIFIED_BY { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string EQUIPMENT_TAG { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string equipment_tag_EQUIPMENT_ID { get; set; }

        [Required(ErrorMessage = "Please enter valid Tag ID")]
        [Range(5, 6, ErrorMessage = "Tag ID is 5-6 numbers")]
        public string material_ELEMENT_MATERIAL_NO { get; set; }


        [Required(ErrorMessage = "Please enter tag")]
        [StringLength(45, ErrorMessage = "Max 45 chars")]
        public string tag { get; set; }

      
        public string stocktaking_STOCKTAKE_ID { get; set; }
    }

}