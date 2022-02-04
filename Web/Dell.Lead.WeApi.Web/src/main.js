import 'primevue/resources/themes/saga-blue/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.css'
import './assets/sass/layout.scss'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import PrimeVue from 'primevue/config'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Menubar from 'primevue/menubar'
import InputNumber from 'primevue/inputnumber'
import Card from 'primevue/card'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Dropdown from 'primevue/dropdown'
import Dialog from 'primevue/dialog'
import Toast from 'primevue/toast'
import Toolbar from 'primevue/toolbar'
import InputMask from 'primevue/inputmask'
import Calendar from 'primevue/calendar'

const app = createApp(App)
app.use(PrimeVue)
app.use(router)

app.component('Button', Button)
app.component('InputText', InputText)
app.component('Menubar', Menubar)
app.component('InputNumber', InputNumber)
app.component('Card', Card)
app.component('DataTable', DataTable)
app.component('Column', Column)
app.component('Dropdown', Dropdown)
app.component('Dialog', Dialog)
app.component('Toast', Toast)
app.component('Toolbar', Toolbar)
app.component('InputMask', InputMask)
app.component('Calendar', Calendar)

app.mount('#app')
