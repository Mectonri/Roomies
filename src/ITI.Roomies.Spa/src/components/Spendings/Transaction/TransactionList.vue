<template>
  <div>
    <h1>Liste des transaction</h1>

    <div>
        <div>
        <el-table :data="getListPage" :page-sizes="[5, 10]">
          <el-table-column label="Description" property="description"></el-table-column>
          <el-table-column label="Price" property="price"></el-table-column>
          <el-table-column label="Date" property="date"></el-table-column>
          <el-table-column label="BudgetId" poperty></el-table-column>

          <el-table-column label="RoomieId"></el-table-column>
          <el-table-column label="Actions">
            <el-button size="mini" @click="Update">Edit</el-button>
          </el-table-column>
         
        </el-table>
         <el-pagination
            background
            layout="prev, pager, next"
            :total="transacDepenseList.length"
            @current-change="changePage"
          >
          </el-pagination>
        </div>

       

    
        <div>
          <el-table :data="transacBudgetList">
            <el-table-column label="Description" property="desc"></el-table-column>
            <el-table-column label="Prix" property="price"></el-table-column>
            <el-table-column label="Date" property="date"></el-table-column>
            <el-table-column label="Sender" property="sRoomieId"></el-table-column>
            <el-table-column label="Receiver" property="rRoomieId"></el-table-column>
            <el-table-column label="Options" property=""> <el-button size="mini" @click="Update">Edit</el-button>
            </el-table-column>

          </el-table>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {
  getAllTransacBudgetAsync,
  getAllTransacDepenseAsync
} from "../../../api/SpendingsApi/TransactionApi";

export default {
  data() {
    return {
      transacDepenseList: [],
      errors: [],
      transacBudgetList: [],
      page: 0
    };
  },
  computed: {
    getListPage(){
      return this.transacDepenseList.slice(this.page * 10, this.page *10 + 10);
    }
  },
  async mounted() {
    this.$root.$on("update", async () => {
      await this.refreshList();
    });
    await this.refreshList();
  },
  methods: {
    async refreshList() {
      try {
        this.transacBudgetList = await getAllTransacBudgetAsync();
        this.transacDepenseList = await getAllTransacDepenseAsync();
      } catch (e) {
        console.error(e);
      }
    },

    changePage(newPage){
      console.log(newPage);
      this.page = newPage - 1;
    },
    async Update() {
      console.log("update");
    }
  }

  
};
</script>

