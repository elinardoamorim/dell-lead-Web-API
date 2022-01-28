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

<style lang="scss">
  body {
    background-color: #edf0f5;
    margin: 0;
  }
  #app {
    .d-menubar {
      .p-menubar {
        border: none;
        border-radius: 0;
        background: linear-gradient(90deg,#0388e5 0,#07bdf4);
      }
    }
  }
</style>