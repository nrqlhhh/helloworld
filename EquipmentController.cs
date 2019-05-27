
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using fyp.Models;
namespace fyp.Controllers
{
    public class EquipmentController : Controller
    {
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
        [Authorize(Roles = "manager,member")]
        public IActionResult ListEquipments()
        {

            // Get a list of all equipment from the database
            List<Equipment> equipment = DBUtl.GetList<Equipment>(
                  @"SELECT * FROM equipment
                  WHERE equipment.EQUIPMENT_ID ");
            return View(equipment);

        }
        [Authorize(Roles = "manager")]
        public IActionResult AddEquipment()
        {
            ViewData["equipment"] = GetListEquipment();
            return View();
        }

        private object GetListEquipment()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult AddEquipment(Equipment newEq)
        {
            if (!ModelState.IsValid)
            {
                ViewData["equipment"] = GetListEquipment();
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("AddEquipment");
            }
            else
            {
                string insert =
                   @"INSERT INTO equipment(ELEMENT_MATERIAL_NO, SERIAL_NO, EQUIPMENT_TYPE_ID, STORAGE_LOCATION, STORAGE_BIN, BOX_LOT_NO, QUANTITY, 
DTE_TIME_CR, DTE_TIME_LAST_MOD, CREATED_BY, MODIFIED_BY, material_ELEMENT_MATERIAL_NO, 
EQUIPMENT_TAG, equipment_tag_EQUIPMENT_ID, tag, stocktaking_STOCKTAKE_ID) 
                 VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}','{7}','{8::yyyy-MM-dd}','{9:yyyy-MM-dd}','{10}','{11}','{12}',{13},'{14}','{15}',{16})";


                int result = DBUtl.ExecSQL(insert, newEq.ELEMENT_MATERIAL_NO, newEq.SERIAL_NO, newEq.EQUIPMENT_TYPE_ID, newEq.STORAGE_LOCATION, newEq.STORAGE_BIN, newEq.BOX_LOT_NO, newEq.QUANTITY,
    newEq.DTE_TIME_CR, newEq.DTE_TIME_LAST_MOD, newEq.CREATED_BY, newEq.MODIFIED_BY, newEq.material_ELEMENT_MATERIAL_NO,
    newEq.EQUIPMENT_TAG, newEq.equipment_tag_EQUIPMENT_ID, newEq.EQUIPMENT_TAG, newEq.stocktaking_STOCKTAKE_ID);  // safe

                if (result == 1)
                {
                    TempData["Message"] = "Equipment Added";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListEquipment");
            }
        }
        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult EditEquipment(string id)
        {
            // Get the record from the database using the id
            string movieSql = @"SELECT ELEMENT_MATERIAL_NO, SERIAL_NO, EQUIPMENT_TYPE_ID, STORAGE_LOCATION, STORAGE_BIN, BOX_LOT_NO, QUANTITY, 
DTE_TIME_CR, DTE_TIME_LAST_MOD, CREATED_BY, MODIFIED_BY, material_ELEMENT_MATERIAL_NO, 
EQUIPMENT_TAG, equipment_tag_EQUIPMENT_ID, tag, stocktaking_STOCKTAKE_ID
                                  FROM equipment
                                 WHERE equipment.EQUIPMENT_ID = '{0}'";

            List<Equipment> lstEquipment = DBUtl.GetList<Equipment>(movieSql, id);

            // If the record is found, pass the model to the View
            if (lstEquipment.Count == 1)
            {
                ViewData["equipment"] = GetListEquipment();
                return View(lstEquipment[0]);
            }
            else
            // Otherwise redirect to the equipment list page
            {
                TempData["Message"] = "Equipment not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("List");
            }

           
}
        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult EditEquipment(Equipment eq)
        {

            // Check the state of the model ((Ref Week 9). 

            // Write the SQL statement

            // Execute the SQL statement in a secure manner

            // Check the result and branch
            // If successful set a TempData success Message and MsgType
            // If unsuccessful, set a TempData message that equals the DBUtl error message

            // Call the action ListEquipments to show the result of the update

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("EditEquipment");
            }
            else
            {
                string update =
                   @"UPDATE equipment
                    SET ELEMENT_MATERIAL_NO='{1}', SERIAL_NO='{2}', EQUIPMENT_TYPE_ID='{3}', STORAGE_LOCATION='{4}', STORAGE_BIN='{5}',BOX_LOT_NO='{6}',QUANTITY='{7}',DTE_TIME_CR='{8::yyyy-MM-dd}',
DTE_TIME_LAST_MOD='{9:yyyy-MM-dd}',CREATED_BY='{10}',MODIFIED_BY='{11}',material_ELEMENT_MATERIAL_NO='{12}',EQUIPMENT_TAG={13},equipment_tag_EQUIPMENT_ID='{14}',tag='{15}',stocktaking_STOCKTAKE_ID={16})
                WHERE EQUIPMENT_ID={0}";
                int res = DBUtl.ExecSQL(update, eq.ELEMENT_MATERIAL_NO, eq.SERIAL_NO, eq.EQUIPMENT_TYPE_ID, eq.STORAGE_LOCATION, eq.STORAGE_BIN, eq.BOX_LOT_NO, eq.QUANTITY,
    eq.DTE_TIME_CR, eq.DTE_TIME_LAST_MOD, eq.CREATED_BY, eq.MODIFIED_BY, eq.material_ELEMENT_MATERIAL_NO,
    eq.EQUIPMENT_TAG, eq.equipment_tag_EQUIPMENT_ID, eq.tag, eq.stocktaking_STOCKTAKE_ID);

                if (res == 1)
                {
                    TempData["Message"] = "Equipment Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListEquipment");
            }



        }
        [Authorize(Roles = "manager")]
        public IActionResult DeleteEquipment(int id)
        {
            string select = @"SELECT * FROM equipment
                              WHERE EQUIPMENT_ID={0}";
            DataTable ds = DBUtl.GetTable(select, id);
            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Equipment record no longer exists.";
                TempData["MsgType"] = "warning";
            }
            else
            {
                string delete = "DELETE FROM equipment WHERE EQUIPMENT_ID={0}";
                int res = DBUtl.ExecSQL(delete, id);
                if (res == 1)
                {
                    TempData["Message"] = "Equipment Deleted";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("ListEquipment");
        }
    }
}