<template>
  <div>
    <div>
      <h1 > Creer un Item</h1>
    </div>

    <div>
      <form @submit="onSubmit($event)">
        <label>Nom</label>
        <input type="text" v-model="item.itemName">
        <label>Prix</label>
        <input type="text" v-model="item.itemName">
        <label>Liste</label>
        <input type="text" v-model="item.itemName">
        <label>Roomie</label>
        <input type="text" v-model="item.roomieId">

       
        <button type="submit">Sauvegarder</button>
      </form>
      
    </div>

  </div>
</template>

<script>
export default {
  data() {
    return{
      item: {},
      id: null,
    }
  },

  async mounted() {
    this.id = this.$route.params.id;

  },

  methods: {
    async onSubmit(event) {
      try{
        await create
                
      }
      finally{

      }

    },

  },
}
</script>
