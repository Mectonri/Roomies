<template>
  <div>
    <div>
      <el-row :gutter="20" justify="center" align="middle">
        <el-col
          :span="6"
          v-for="(category, index) in categoryList"
          :key="index"
          :label="category.categoryName"
          :value="category"
        >
          <div class="grid-content bg-purple">
            <p>
              <el-row justify="center" align="middle">
                <el-popover
                  transition="el-fade-in-linear"
                  placement="bottom"
                  :title="category.categoryName"
                  width="200"
                  trigger="click"
                  
                >
                  <el-button slot="reference">
                    {{category.categoryName}}
                    
                    <img
                      :src="iconPath + '/' + category.iconName + '.png'"
                      width="50"
                      height="50"
                      :content="category"
                      @click="print()"
                    >
                  </el-button>
                  <div>
                    <el-popover placement="top" width="160" v-model="visible">
                      <p>Are you sure to delete this?</p>
                      <div style="text-align: right; margin: 0">
                        <el-button size="mini" type="text" @click="visible = false">cancel</el-button>
                        <el-button type="primary" size="mini" @click="visible = false">confirm</el-button>
                      </div>
                      <el-button slot="reference">Delete</el-button>
                    </el-popover>

                    <el-button>edit</el-button>
                  </div>
                </el-popover>
              </el-row>
            </p>
            <div class="w-100"></div>
          </div>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
import { getCategoriesAsync } from "../../../api/SpendingsApi/CategoryApi";

export default {
  data() {
    return {
      categoryList: [],
      collocId: null,
      iconPath: "http://localhost:5000/Pictures/Icons",
      visible: false,

    };
  },

  async mounted() {
    this.collocId = this.$currColloc.collocId;
    this.$root.$on("updateCategoryList", async () => {
      await this.refreshList();
    });
    await this.refreshList();
  },

  methods: {
    async refreshList() {
      try {
        this.categoryList = await getCategoriesAsync(this.collocId);
      } catch (e) {
        console.error(e);
      }
    },
   
  }
};
</script>

<style lang="scss" scoped>
// .el-row {
//   margin-bottom: 20px;
//   &:last-child {
//     margin-bottom: 0;
//   }
// }
// .el-col {
//   border-radius: 4px;
// }
// .bg-purple-dark {
//   background: #99a9bf;
// }
// .bg-purple {
//   background: #d3dce6;
// }
// .bg-purple-light {
//   background: #e5e9f2;
// }
// .grid-content {
//   border-radius: 4px;
//   min-height: 36px;
// }
// .row-bg {
//   padding: 10px 0;
//   background-color: #f9fafc;
// }
//
</style>
