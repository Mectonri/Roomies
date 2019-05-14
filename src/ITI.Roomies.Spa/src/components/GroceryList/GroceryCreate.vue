<template>
  <div>
    <header>
      <h2>Creer une liste de courses</h2>
    </header>

    <div>
      <form>
        <div>
          <label class="required">Nom</label>
          <br>
          <input class="form-control" type="text" v-model="course.courseName" required>
        </div>
        <br>
        <div>
          <label class="required">Date</label>
          <br>
          <input class="form-control" type="datetime-local" v-model="course.courseDate" required>
        </div>
        <br>
        <button class="btn btn-dark" type="submit" @click="onSubmit">Sauvegarder</button>
      </form>
    </div>
  </div>
</template>

<script>
import AuthService from "../../services/AuthService";
import {
  createGroceryListAsync,
  getGroceryListByIdAsync
} from "../../api/GroceriesApi";

export default {
  data() {
    return {
      course: {}
    };
  },

  methods: {
    // async onSubmit(event) {
    //   try {
    //     await createGroceryListAsync(this.course);
    //   } catch (e) {
    //     console.log(e);
    //   }
    // },

    async onSubmit() {
      event.preventDefault();

      try {
        await createGroceryListAsync(this.course);
        this.$router.push("/course");
      } catch (e) {
        console.log(e);
      }
    }
  }
};
</script>
