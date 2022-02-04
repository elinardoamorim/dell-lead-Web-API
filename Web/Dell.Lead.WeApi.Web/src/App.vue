<template>
  <div class="d-menubar">
    <Menubar :model="items">
      <template #start>
        <img alt="logo" src="https://leadfortaleza.com.br/assets/logos/logo-lead-dell-horizontal.svg" height="25" class="p-mr-2">
      </template>
    </Menubar>
  </div>
  <router-view />
</template>

<script>
import items from './data/menu';

  export default {
    name: 'App',
    data() {
      return {
        items
      }
    }
  }
</script>