import axios from 'axios'

export default class ViaCepServices {
  getViaCep(cep) {
    return axios.get(`https://viacep.com.br/ws/${cep}/json/`)
  }
}
