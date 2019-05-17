    <div >
     <h1 class="text-center">{{$t('Welcome')}} {{$t('Profil')}}</h1>
    </div>
    <div class="centerBox">
      <h2>{{$t('pic')}}</h2>
      <img class="profilePicture" :src="this.env+'/'+this.roomiePic" alt="Vous n'avez pas de photos de profil">
      <br>
    </div>

      <div >
        <h1 class="text-center">{{$t('Welcome')}} {{$t('Profil')}}</h1>
      </div>
 
    <div>
      <el-button  icon="el-icon-edit"  @click="editPic"></el-button>
    </div>
    <div>
      <H2>
        {{$t('Profil')}}
        <el-button  icon="el-icon-edit"  @click="editProfile" circle>
        </el-button>
      </H2>

      <table>
        <div v-if="roomie == null">
          <tr>
            <h1> {{$t('erreur')}}</h1>
          </tr>
        </div>

        <div v-else>
        
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
           <tr>
             <th></th>
             <td></td>
           </tr>
                
        </div>
        
      </table>
    </div>

      <div>
        <Language></Language>
    </div>