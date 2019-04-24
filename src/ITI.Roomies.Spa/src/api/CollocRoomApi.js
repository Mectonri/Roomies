import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/CollocRoom";

export async function addCollRoomAsync(model, userInfo){
    return await postAsync(endpoint, model, userInfo);
}