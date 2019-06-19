<template class='profile'>
  <div v-if="!state">
    <header>
      <!-- <h2>{{$t('Welcome')}} {{$t('Profil')}}</h2> -->
      <h2>{{$t('Profil')}}</h2>
    </header>
    <el-container>
      <el-row>
        <el-col>
          <div class="centerBox">
            <header style="padding-left: 2rem;">
              <h5>{{$t('pic')}}</h5>
            </header>
            <img
              width="200px"
              height="200px"
              class="profilePicture"
              :src="this.env+'/'+this.roomiePic"
              alt="Vous n'avez pas de photo de profil"
            >
            <br>
          </div>
        </el-col>

        <el-col>
          <div v-if="roomie.description == null || ''">
            <tr>{{$t("nullDesc")}}</tr>
          </div>
          <div v-else>
            <tr>
              <th>Description</th>
              <td>{{roomie.description}}</td>
            </tr>
          </div>
        </el-col>
      </el-row>

      <el-row>
        <el-col>
          <table>
            <div v-if="roomie == null">
              <tr>
                <h1>{{$t('erreur')}}</h1>
              </tr>
            </div>

            <div v-else>
              <br>
              <br>
              <tr>
                <th>{{$t('Nom')}}</th>
                <td>{{roomie.lastName}}</td>
              </tr>
              <tr>
                <th>{{$t('Prenom')}}</th>
                <td>{{roomie.firstName}}</td>
              </tr>
              <tr>
                <th>{{$t('Bday')}}</th>
                <td>{{roomie.birthDate}}</td>
              </tr>
              <tr>
                <th>Email</th>
                <td>{{roomie.email}}</td>
              </tr>
              <tr>
                <th>{{$t("number")}}</th>
                <td>{{roomie.phone}}</td>
              </tr>
            </div>
          </table>
        </el-col>
      </el-row>
    </el-container>
  </div>
  <div v-else>
    <loading/>
  </div>
</template>

<script>
import i18n from "../../plugins/i18n";
import { getProfileAsync, getRoomiePicAsync } from "../../api/RoomiesApi";
import AuthService from "../../services/AuthService";
import Loading from "../../components/Utility/Loading.vue";
import monthFr from "../../components/Utility/month.js";

export default {
  components: {
    Loading
  },

  data() {
    return {
      env: process.env.VUE_APP_BACKEND,
      roomieId: null,
      roomie: null,
      roomiePic: "",
      defaultPic: null,
      state: true
    };
  },

  async mounted() {
    this.state = true;
    this.monthList = require("../../components/Utility/month.js");
    try {
      this.roomie = await getProfileAsync();
      this.roomie.birthDate = this.dateToFrDisplay(
        this.sqlToJsDate(this.roomie.birthDate)
      );
      this.roomiePic = await getRoomiePicAsync();
    } catch (e) {
      console.log(e);
    } finally {
      this.state = false;
    }
  },

  methods: {
    async editPic() {},
    async editProfile() {
      this.$router.replace("/roomie/");
    },

    sqlToJsDate(sqlDate) {
      sqlDate = sqlDate.replace("T", " ");

      //sqlDate in SQL DATETIME format ("yyyy-mm-dd hh:mm:ss.ms")
      var sqlDateArr1 = sqlDate.split("-");
      //format of sqlDateArr1[] = ['yyyy','mm','dd hh:mm:ms']
      var sYear = sqlDateArr1[0];
      var sMonth = (Number(sqlDateArr1[1]) - 1).toString();
      var sqlDateArr2 = sqlDateArr1[2].split(" ");
      //format of sqlDateArr2[] = ['dd', 'hh:mm:ss.ms']
      var sDay = sqlDateArr2[0];
      var sqlDateArr3 = sqlDateArr2[1].split(":");
      //format of sqlDateArr3[] = ['hh','mm','ss.ms']
      var sHour = sqlDateArr3[0];
      var sMinute = sqlDateArr3[1];
      // var sqlDateArr4 = sqlDateArr3[2].split(".");
      //format of sqlDateArr4[] = ['ss','ms']
      var sSecond = 0;
      var sMillisecond = 0;

      return new Date(
        sYear,
        sMonth,
        sDay,
        sHour,
        sMinute,
        sSecond,
        sMillisecond
      );
    },

    dateToFrDisplay(laDate) {
      let dayToDisplay =
        laDate.getDate().toString().length == 1
          ? "0" + laDate.getDate().toString()
          : laDate.getDate();
      // let monthToDisplay = laDate.getMonth() + 1;
      // monthToDisplay =
      //   monthToDisplay.toString().length == 1
      //     ? "0" + monthToDisplay.toString()
      //     : monthToDisplay;
      let monthToDisplay = this.monthList.monthFr[laDate.getMonth()];
      let yearToDisplay = laDate.getFullYear();
      return dayToDisplay + " " + monthToDisplay + " " + yearToDisplay;
    }
  }
};
</script>
<style scoped>

</style>

