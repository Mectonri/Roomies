<template>
  <div>
    <v-calendar color= "red" is-dark/>
  <div>
</template>



<script>
  import Vcalendar from 'v-calendar';

  
export default {
  data() {
    return {
      attrs: [
        {
          key: 'today',
          highlight: {
            backgroundColor: '#ff8080',
            // Other properties are available too, like `height` & `borderRadius`
          },
          dates: new Date(2018, 0, 1)
        }
      ],
    };
  },
}
</script>