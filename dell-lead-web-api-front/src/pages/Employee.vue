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
              <InputNumber id="cpf" v-model="cpf" disabled />
            </span>
            <span class="p-field">
              <h5>Nome</h5>
              <InputText id="name" type="text" v-model="name" />
            </span>
            <span class="p-field">
              <h5>Data de Nascimento</h5>
              <InputNumber id="birthDate" v-model="birthDate" />
            </span>
            <span class="p-field">
              <h5>Telefone</h5>
              <InputNumber id="phone" v-model="phone" />
            </span>
            <span class="p-field">
              <h5>Gênero</h5>
              <InputText id="birthDate" type="text" v-model="birthDate" />
            </span>
            <span class="p-field">
              <h5>Authors</h5>
              <!--<Dropdown 
                v-model="selectedAuthor" 
                :options="optionsAuthors" 
                optionLabel="name"  
                placeholder="Select a Author" 
              />-->
              <Dropdown 
								v-model="selectedAuthor" 
								:options="optionsAuthors" 
								optionLabel="name" 
								:filter="true" 
								placeholder="Select a Author" 
								:showClear="true"
							>
								<template #value="slotProps">
									<div class="country-item country-item-value" v-if="slotProps.value">
										<div>{{slotProps.value.name}}</div>
									</div>
									<span v-else>
										{{slotProps.placeholder}}
									</span>
								</template>
								<template #option="slotProps">
									<div class="country-item">
										<div>{{slotProps.option.name}}</div>
									</div>
								</template>
            </Dropdown>
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
        :value="books" 
        :scrollable="true" 
        scrollHeight="400px" 
        responsiveLayout="scroll"
        filterDisplay="menu"
        v-model:filters="filters"
      >
        <Column field="id" header="ID" :sortable="true"></Column>
        <Column 
          field="title" 
          filterField="title"
          :sortable="true" 
          :showFilterMatchModes="false"
          header="Title"
        >
          <template #filter="{filterModel}">
              <div class="mb-3 font-bold">Title</div>
              <InputText v-model="filterModel.value"/>
          </template>
        </Column>
        <Column 
          field="price" 
          header="Price"
          filterField="price" 
          :showFilterMatchModes="false"  
        >
          <template #filter="{filterModel}">
              <div class="mb-3 font-bold">Price</div>
              <InputText v-model="filterModel.value"/>
          </template>
        </Column>
        <Column field="author.name" header="Author"></Column>
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
export default {
  name: 'Employee'
}
</script>