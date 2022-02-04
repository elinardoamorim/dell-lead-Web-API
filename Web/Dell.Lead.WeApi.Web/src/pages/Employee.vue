<template>
	<Toast />
	<Dialog header="Confirmation" v-model:visible="displayConfirmation" :style="{width: '350px'}" :modal="true">
		<div class="confirmation-content">
			<i class="pi pi-exclamation-triangle p-mr-3" style="font-size: 2rem" />
			<span>Are you sure you want to proceed?</span>
		</div>
		<template #footer>
			<Button label="No" icon="pi pi-times" @click="cancelBook" class="p-button-text"/>
			<Button label="Yes" icon="pi pi-check" @click="deleteBook" class="p-button-text" autofocus />
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
              <InputMask id="cpf" v-model="cpf" mask="999.999.999-99" placeholder="000.000.000-00"/>
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
              <InputMask id="phone" mask="(99)9.9999-9999" placeholder="(99)9.9999-9999" v-model="phone" />
            </span>
            <span class="p-field">
              <h5>Gênero</h5>
              <InputText id="gender" type="text" v-model="gender" />
            </span>
            <span class="p-field">
              <h5>CEP</h5>
              <InputText id="cep" v-model="cep" @keyup.enter="searchCep" />
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
              <InputText id="district" type="text" v-model="district" />
            </span>
            <span class="p-field">
              <h5>Cidade</h5>
              <InputText id="city" type="text" v-model="city" />
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
            <Button icon="pi pi-pencil" class="p-button-rounded p-button-success p-mr-2" @click="editBook(slotProps.data)" />
            <Button icon="pi pi-trash" class="p-button-rounded p-button-warning" @click="confirmDeleteBook(slotProps.data.id)" />
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
      viaCep: {
        street: ''
      },
      id: null,
      name_full: '',
      cpf: null,
      birth_date: '',
      phone: null,
      gender: '',
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
  }
  }
}
</script>