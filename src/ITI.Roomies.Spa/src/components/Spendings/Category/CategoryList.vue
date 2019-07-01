<template>
  <div>

<div>
  <h1>Liste des Categories</h1>
</div>

    <div>
      <el-row :gutter="20" justify="center" align="middle">
        <el-col
          :span="6"
          v-for="(category, index) in categoryList"
          :key="index"
        
         
        >
          <div class="grid-content bg-purple">
            <p>
              <el-row justify="center" align="middle">
                <el-popover
                  transition="el-fade-in-linear"
                  placement="bottom"
                 
                  width="200"
                  trigger="click" 
                >
                
                  <CategoryInfo :Options="category"></CategoryInfo>   
                  <el-button slot="reference" >
                    
                    <img
                      :src="iconPath + '/' + category.iconName + '.png'"
                      width="50"
                      height="50"
                    >
                  </el-button>
                  <div>
                    <el-popover placement="top" width="170" v-model="visible">
                      <p>Are you sure to delete this?</p>
                      <div style="text-align: right; margin: 0">
                        <el-button size="mini" type="text" @click="visible = false">cancel</el-button>
                        <el-button type="primary" size="mini" @click="visible = false; deleteCategory(category.categoryId)">confirm</el-button>
                      </div>
                      <el-button slot="reference">Delete</el-button>
                    </el-popover>
                    <div>
                      <router-link :to="`edit/${category.categoryId}`">
                      <el-button>edit</el-button>
                      </router-link>
                    </div>
                   
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
import { getCategoriesAsync, deleteCategoryAsync} from "../../../api/SpendingsApi/CategoryApi";
import { deleteAsync } from '../../../helpers/apiHelper';
import CategoryInfo from '../Category/CategoryInfo.vue';

export default {
  components:{
    CategoryInfo
  },
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
    async deleteCategory(categoryId){
     
      try{
        await deleteCategoryAsync(categoryId);
      }catch(e){
          console.error(e);
      }finally{
        await this.refreshList();
      }
    }
  }
};
</script>

<style lang="scss" scoped>

.el-button{
  min-width: 10rem;
  max-width: 10rem;
}


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
