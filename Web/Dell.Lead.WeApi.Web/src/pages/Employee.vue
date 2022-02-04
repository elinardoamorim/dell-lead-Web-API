<template>
	<Toast />
	<Dialog header="Confirmation" v-model:visible="displayConfirmation" :style="{width: '350px'}" :modal="true">
		<div class="confirmation-content">
			<i class="pi pi-exclamation-triangle p-mr-3" style="font-size: 2rem" />
			<span>Are you sure you want to proceed?</span>
		</div>
		<template #footer>
			<Button label="No" icon="pi pi-times" @click="cancelEmployee" class="p-button-text"/>
			<Button label="Yes" icon="pi pi-check" @click="deleteEmployee" class="p-button-text" autofocus />
		</template>
	</Dialog>
  <div class="content">
    <div class="card-content">
      <Card>
        <template #title>
          <h1>Employee</h1>
        </template>
        <template #content> 
          <div class="content-filter">
            <span class="p-field">
              <h5>CPF</h5>
              <InputNumber id="cpf" v-model="cpf"/>
            </span>
            <span class="p-field">
              <h5>Nome</h5>
              <InputText id="name_full" type="text" v-model="name_full" />
            </span>
            <span class="p-field">
              <h5>Data de Nascimento</h5>
              <Calendar id="birth_date" v-model="birth_date"  dateFormat="mm-dd-yy" />
            </span>
            <span class="p-field">
              <h5>Telefone</h5>
              <InputNumber id="phone" v-model="phone" />
            </span>
            <span class="p-field">
              <h5>Gênero</h5>
              <InputText id="gender" type="text" v-model="gender" />
            </span>
            <span class="p-field">
              <h5>CEP</h5>
              <InputText id="cep" v-model="cep" @keyup.capture="searchCep" />
            </span>
            <span class="p-field">
              <h5>Rua</h5>
              <InputText id="street" type="text" v-model="viaCep.street" />
            </span>
            <span class="p-field">
              <h5>Número</h5>
              <InputNumber id="number" v-model="number" />
            </span>
            <span class="p-field">
              <h5>Bairro</h5>
              <InputText id="district" type="text" v-model="viaCep.district" />
            </span>
            <span class="p-field">
              <h5>Cidade</h5>
              <InputText id="city" type="text" v-model="viaCep.city" />
            </span>
            <span class="p-field">
              <h5>Estado</h5>
              <InputMask id="state" mask="**" v-model="viaCep.state" />
            </span>
          </div> 
          <div class="d-button">
            <Button label="Save" @click="save" v-if="isSave"/>
            <Button label="Update" @click="update" v-else/>
          </div>    
        </template>
      </Card>
    </div>
    
    <div class="d-datatable">
      <DataTable 
        :value="employees" 
        :scrollable="true" 
        scrollHeight="400px" 
        responsiveLayout="scroll"
        filterDisplay="menu"
        v-model:filters="filters"
      >
        <Column field="code" header="ID" :sortable="true"></Column>
        <Column 
          field="name_full" 
          filterField="name_full"
          :sortable="true" 
          :showFilterMatchModes="false"
          header="Nome"
        >
          <template #filter="{filterModel}">
              <div class="mb-3 font-bold">Nome</div>
              <InputText v-model="filterModel.value"/>
          </template>
        </Column>
        <Column 
          field="cpf" 
          header="CPF"
          filterField="cpf" 
          :showFilterMatchModes="false"  
        >
          <template #filter="{filterModel}">
              <div class="mb-3 font-bold">CPF</div>
              <InputText v-model="filterModel.value"/>
          </template>
        </Column>
        <Column field="birth_date" header="Data de nascimento"></Column>
        <Column field="phone" header="Telefone"></Column>
        <Column field="gender" header="Gênero"></Column>
        <Column field="address.street" header="Rua"></Column>
        <Column field="address.number" header="Número"></Column>
        <Column field="address.district" header="Bairro"></Column>
        <Column field="address.city" header="Cidade"></Column>
        <Column field="address.state" header="Estado"></Column>
        <Column field="address.cep" header="CEP"></Column>
        <Column :exportable="false">
          <template #body="slotProps">
            <Button icon="pi pi-pencil" class="p-button-rounded p-button-success p-mr-2" @click="editEmployee(slotProps.data)" />
            <Button icon="pi pi-trash" class="p-button-rounded p-button-warning" @click="confirmDeleteEmployee(slotProps.data.cpf)" />
          </template>
        </Column>
        <template #empty>
            No records found.
        </template>
      </DataTable>
    </div>
  </div>
</template>

<script>
import { FilterMatchMode } from 'primevue/api';
import EmployeeService from '../services/EmployeeService'
import ViaCepService from '../services/ViaCepService'

export default {
  name: 'Employee',
  data() {
    return {
      employee: null,
      employees: [],
      addressId: null,
      viaCep: {
        street: '',
        district: '',
        city: '',
        state: ''
      },
      id: null,
      cep: null,
      name_full: '',
      cpf: null,
      birth_date: '',
      phone: null,
      gender: '',
      number: null,
      isSave: true,
      Address: [],
      filters: []
    }
  },
  employeeService: null,
  created(){
    this.employeeService = new EmployeeService();
    this.viaCepService = new ViaCepService();
    this.filters = this.initFilters();
  },
  async mounted(){
    await this.requestGetEmployees();
  },
  methods: {
  initFilters() {
    return {
      'name_full': { value: null, matchMode: FilterMatchMode.CONTAINS },
      'cpf': { value: null, matchMode: FilterMatchMode.CONTAINS },
    };
  },
  showSuccess() {
      this.$toast.add({severity:'success', summary: 'Success Message', detail:'Message Content', life: 3000});
  },
  showError() {
      this.$toast.add({severity:'error', summary: 'Error Message', detail:'Message Content', life: 3000});
  },
  async save(){
    let employee = {
      'name_full': this.name_full,
      'cpf': this.cpf,
      'birth_date': this.birth_date,
      'phone': this.phone,
      'gender': this.gender,
    };
    employee.Address = {
      'street': this.viaCep.street,
      'district': this.viaCep.district,
      'city': this.viaCep.city,
      'state': this.viaCep.state,
      'number': this.number,
      'cep': this.cep
      };
      this.clearField();
      await this.requestPostEmployee(employee);
  },
  async update() {
      let employee = {
      'code': this.id, 
      'name_full': this.name_full,
      'cpf': this.cpf,
      'birth_date': this.birth_date,
      'phone': this.phone,
      'gender': this.gender,
    };
    employee.Address = {
      'code': this.addressId,
      'street': this.viaCep.street,
      'district': this.viaCep.district,
      'city': this.viaCep.city,
      'state': this.viaCep.state,
      'number': this.number,
      'cep': this.cep
      };
      this.clearField();
      this.isSave = true;
      await this.requestPutEmployee(employee);
  },
  editEmployee(data) {
    this.id = data.code;
    this.name_full = data.name_full;
    this.cpf = data.cpf;
    this.birth_date = data.birth_date;
    this.phone = data.phone;
    this.gender = data.gender;
    this.addressId = data.address.code;
    this.cep = data.address.cep;
    this.number = data.address.number;
    this.viaCep.street = data.address.street;
    this.viaCep.city = data.address.city;
    this.viaCep.district = data.address.district;
    this.viaCep.state = data.address.state;

    this.isSave = false;
  },
  async deleteEmployee(){
    await this.requestDeleteEmployee(this.cpf);
    this.displayConfirmation = false;
    this.clearField();
    this.isSave = true;
  },
  cancelEmployee() {
    this.cpf= null;
    this.displayConfirmation = false;
    this.clearField();
    this.isSave = true;
  },
  confirmDeleteEmployee(value) {
    this.cpf = value;
    this.displayConfirmation = true;
  },
  clearField() {
    this.cpf = null,
    this.name_full = '',
    this.birth_date = '',
    this.phone = null,
    this.gender = '',
    this.number = null,
    this.cep = null,
    this.viaCep.street = '',
    this.viaCep.district = '',
    this.viaCep.city = '',
    this.viaCep.state = ''
  },
  async searchCep(){
    await this.requestGetViaCep(this.cep);
    console.log(this.cep);
    console.log(this.viaCep);
  },
  async requestGetEmployees(){
    await this.employeeService.getEmployees()
      .then(response => {
        let data = response.data;
        this.employees = [];
        data.forEach(employee => {
          this.employees.push({...employee})
        });
      })
      .catch(() => {
        console.log('Ocorreu um erro!');
      });
  },
  async requestGetViaCep(cep){
    await this.viaCepService.getViaCep(cep)
      .then(response => {
        let data = {...response.data};
        this.viaCep = {
          city: data.localidade,
          district: data.bairro,
          street: data.logradouro,
          state: data.uf
        };
      })
      .catch(() => {
        console.log('Ocorreu um erro!')
      });
    },
    async requestPostEmployee(employee = {}){
      await this.employeeService.postEmployee(employee)
        .then(() => {
          this.requestGetEmployees();
          this.showSuccess();
        })
        .catch(error => {
          this.$toast.add({severity:'error', summary: 'Error Register', detail:`${error.response.data.title}`, life: 3000});
        });
    },
    async requestPutEmployee(employee = {}){
      await this.employeeService.putEmployee(employee)
        .then(() => {
          this.requestGetEmployees();
          this.showSuccess();
        })
        .catch(() => {
          this.showError();
        });
    },
    async requestDeleteEmployee(cpf){
      await this.employeeService.deleteEmploye(cpf)
        .then(() => {
          this.requestGetEmployees();
          this.showSuccess();
        })
        .catch(() => {
          this.showError();
        });
    }
  }
}
</script>