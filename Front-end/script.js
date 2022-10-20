


async function editEmployee(id)
{
    sessionStorage["empIDToEdit"] = id;
    new window.bootstrap.Modal(document.getElementById('editEmpDiv')).show();
    let resp = await fetch("https://localhost:44302/api/Employee/" + sessionStorage["empIDToEdit"]);
    let emp = await resp.json();

    document.getElementById("id").value = emp.ID;
    document.getElementById("fname").value = emp.FirstName;
    document.getElementById("lname").value = emp.LastName;
    document.getElementById("startYear").value = emp.StartWorkYear;
    document.getElementById("department").value = emp.Department;

    sessionStorage["deparEditEmp"] = emp.Department;
}





async function updateEmployee()
{
    // Get the new department data
    let respDepar = await fetch("https://localhost:44302/api/Department?depar=" + document.getElementById("department").value);
    let deparData = await respDepar.json();

    //Check if user change the departmrnt 
    if(sessionStorage["deparEditEmp"] != document.getElementById("department").value)
    {
      // Show error when enter wrong department
        if(deparData == null)
        {
          new window.bootstrap.Modal(document.getElementById('wrongDeparPopUp')).show();
          return
        }
          // Check if the employee was the manager of the last department
          let respLastDepar = await fetch("https://localhost:44302/api/Department?depar=" + sessionStorage["deparEditEmp"]);
          lastDeparData = await respLastDepar.json();

          // block from change department for manager
          if(lastDeparData.ManagerID == document.getElementById("id").value)
          {
            new window.bootstrap.Modal(document.getElementById('cantChangeDeparManag')).show();
            return
          }
    }
      let obj = {FirstName : document.getElementById("fname").value,
                 LastName : document.getElementById("lname").value,
                 StartWorkYear : parseInt(document.getElementById("startYear").value),
                 DepartmentID : deparData.ID}
  
      let fetchParams = {method : 'PUT',
                      body : JSON.stringify(obj),
                      headers : {"Content-Type" : "application/json"}}
  
      let resp = await fetch("https://localhost:44302/api/Employee/" + sessionStorage["empIDToEdit"], fetchParams);
      let data =  await resp.json()
  
      document.location.href = "Employee.html"; 
    }
  


function showFullName()
{
    let fName = document.getElementById("pFullName")
    fName.innerText = sessionStorage["userFullName"]

    let actionsLeft = document.getElementById("actionLeftNav")
    let actionsTotal = document.getElementById("actionTotlNav")

    actionsLeft.innerText = parseInt(sessionStorage["NumOfAction"]) - parseInt(sessionStorage["ActionDone"])
    actionsTotal.innerText = parseInt(sessionStorage["NumOfAction"])
}


function blockUser()
{ 
  if(window.location.href == "http://127.0.0.1:5500/Log-in%20Page/Log-in.html?#")
  {
    document.getElementById("maxActionDiv").style.display = "block";
    console.log("cant log in ");
  }
  
  else
  {
    document.location.href = "Log-in.html";
  }
}


async function checkForMaxCredit()
{
  
    let resp = await fetch("https://localhost:44302/api/User/" + sessionStorage["currentUserID"]);
    let user =  await resp.json()

    console.log("Action make :" + user.ActionDone);

    if(user.ActionDone > user.NumOfAction)
    {

      blockUser()
      return true
    }
    else
    {
      return false
    }
}


async function useCredit()
{
  sessionStorage["ActionDone"] =  parseInt(sessionStorage["ActionDone"]) + 1;

  let respUser = await fetch("https://localhost:44302/api/User/" + sessionStorage["currentUserID"]);
  let userData = await respUser.json();
  
  let obj = {FullName : userData.FullName,
             UserName : userData.UserName,
             Password : userData.Password,
             NumOfAction : userData.NumOfAction,
             ActionDone : userData.ActionDone + 1,
             LastActionDate : userData.LastActionDate}

    let fetchParams = {method : 'PUT',
                    body : JSON.stringify(obj),
                    headers : {"Content-Type" : "application/json"}}

    

    let respUpdate = await fetch("https://localhost:44302/api/User/" + sessionStorage["currentUserID"], fetchParams);
    let data =  await respUpdate.json()

    checkForMaxCredit()
}


function logOut()
{
    document.location.href = "Log-in.html";
}









