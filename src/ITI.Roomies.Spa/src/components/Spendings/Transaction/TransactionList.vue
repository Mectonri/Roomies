<template>
  <div>
    <div>
      <h1>Liste des transaction</h1>
    </div>

    <div>
      <div>
        <el-table :data="getTDepensePage" :page-sizes="[5, 10]">
          <el-table-column label="Description" property="description"></el-table-column>
          <el-table-column label="Price" property="price"></el-table-column>
          <el-table-column label="Date" property="date"></el-table-column>
          <el-table-column label="BudgetId" poperty></el-table-column>

          <el-table-column label="RoomieId"></el-table-column>
          <el-table-column label="Actions">
            <template slot-scope="scope">
              <router-link :to="`transaction/edit/${getTDepensePage[scope.$index].tDepenseId}`">
                <el-button
                  size="mini"
                  @click="updateTDepense(getTDepensePage[scope.$index].tDepenseId)"
                >Edit</el-button>
              </router-link>
              <el-button size="mini" @click="deleteTDepense(getTDepensePage[scope.$index].tDepenseId)">Delete</el-button>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          background
          layout="prev, pager, next"
          :total="transacDepenseList.length"
          @current-change="changePage"
        ></el-pagination>
      </div>

      <div>
        <el-table :data="getTBudgetPage">
          <el-table-column label="Description" property="desc"></el-table-column>
          <el-table-column label="Prix" property="price"></el-table-column>
          <el-table-column label="Date" property="date"></el-table-column>
          <el-table-column label="Sender" property="sRoomieId"></el-table-column>
          <el-table-column label="Receiver" property="rRoomieId"></el-table-column>
          <el-table-column label="Options" property>
            <template slot-scope="scope1">
              <routeur-link :to="`tBudget/edit/${getTBudgetPage[scope1.$index].tBudgetId}`">
            <el-button size="mini" @click="updateTBudget(getTBudgetPage[scope1.$index].tBudgetId)">Edit</el-button>
            </routeur-link>
            <el-button size="mini" @click="deleteTBudget(getTBudgetList[scope1.$index].tBudgetId)"></el-button>
            </template>
          </el-table-column>
        </el-table>
      </div>
    </div>
  </div>
</template>
<script>
import {
  getAllTransacBudgetAsync,
  getAllTransacDepenseAsync,
  deleteTransacBudgetAsync,
  deleteTDepenseAsync,
  updateTransacBudgetAsync,
  updateTransacDepenseAsync
} from "../../../api/SpendingsApi/TransactionApi";

export default {
  data() {
    return {
      transacDepenseList: [],
      errors: [],
      transacBudgetList: [],
      page: 0,
    };
  },
  computed: {
    getTDepensePage() {
      return this.transacDepenseList.slice(this.page * 10, this.page * 10 + 10);
    },
    getTBudgetPage(){
      return this.transacBudgetList.slice(this.page * 10, this.page * 10 + 10);
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

    changePage(newPage) {
      console.log(newPage);
      this.page = newPage - 1;
    },
    async Update() {
      console.log("update");
    },
    async Delete() {
      console.log("delete");
      try {
        await this.refreshList();
      } catch (e) {
        console.log(e);
      } finally {
        await this.refreshList();
      }
    },
    async deleteTDepense(TDepenseId) {
      console.log("delete");
      console.log(TDepenseId);
      try {
        
        await deleteTDepenseAsync(TDepenseId);
      } catch (e) {
        console.error(e);
      } finally {
        await this.refreshList();
      }
    },
    async deleteTBudget() {
      try {
        await this.deleteTransacBudgetAsync();
      } catch (e) {
        console.error(e);
      } finally {
        await this.refreshList();
      }
    },
    async updateTDepense(id) {
      try {
        await this.updateTransactionDepense(id);
      } catch (e) {
        console.error(e);
      } finally {
        await this.refreshList();
      }
    },
    async updateTBudget(id) {
      try {
        await this.updateTransactionBudget(id);
      } catch (e) {
        console.error(e);
      } finally {
        await this.refreshList();
      }
    }
  }
};
</script>

