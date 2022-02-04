import axios from 'axios'

export default class EmployeeServices {
  getEmployees() {
    return axios.get('https://localhost:44373/api/v1/employees')
  }
}
