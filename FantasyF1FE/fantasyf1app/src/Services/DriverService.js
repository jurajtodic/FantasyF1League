import httpCommon from './http-common.js';
import http from './http-common.js';

const getDriver = async id =>{
    return await http.get(`/driver_By_Id_list?id=${id}`);
}

const createDriver = async data =>{
    return await http.post("/add_driver",data);
}

const updateDriver = async (id,data) => {
    return await http.put(`/update_driver?id=${id}`,data);
}
//update_driver?id={id}

const deleteDriver = async id => {
    return await http.delete(`/delete_driver?id=${id}`);
}

const findDriverByFilter = async (item) => {
    var string = "/driver_filter_list?";

    var checkerForLastDollarSign = Object.keys(item).length
    var counterForLastDollarSign = 0;
    Object.keys(item).forEach((i) => {
    var counterForToggle = false;    
    if(Object.keys(i)!==0){
            Object.values(item[i]).forEach((obj)=>{
                if(counterForToggle === false){
                    string+=`${obj}=`;
                    counterForToggle=true;
                }
                else {
                    if(counterForLastDollarSign!==checkerForLastDollarSign-1){
                        string+=`${obj}&`;
                        counterForLastDollarSign++;
                    }
                    else {string+=`${obj}`}
                }
                
            })
        }
        
    })

    return await http.get(string);

    /*if(Object.keys(item.page)!==0){
        string = string + `${item.page.name}=${item.page.value}`;
        return await http.get(string);
    }
    else {
        return await http.get("/driver_filter_list");
    }*/
    //tu je krajnja zagrada
}

const getAllConstructors = async () =>{
    return await http.get("/team_list");
}

const getAllScoringRules = async () =>{
    return await http.get("/scoringrules_list");
}

function getAllDrivers() {
    return http.get("/driver_filter_list");
} 
const updateDriverPoints = async (id,position) => {
    return await http.put(`/add_total_points_by_driver_id?id=${id}&position=${position}`)
}

const DriverService = {
    findDriverByFilter,
    getDriver,
    createDriver,
    updateDriver,
    deleteDriver,
    getAllConstructors,
    getAllScoringRules,
    getAllDrivers,
    updateDriverPoints
  };
  
export default DriverService;