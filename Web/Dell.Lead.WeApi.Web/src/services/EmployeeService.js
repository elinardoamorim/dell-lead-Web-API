import axios from 'axios'

export default class EmployeeServices {
  getEmployees() {
    return axios.get('https://localhost:44373/api/v1/employees')
  }

  postEmployee(employee = {}) {
    return axios.post('https://localhost:44373/api/v1/employees', employee)
  }

  putEmployee(employee = {}) {
    return axios.put('https://localhost:44373/api/v1/employees', employee)
  }
}
