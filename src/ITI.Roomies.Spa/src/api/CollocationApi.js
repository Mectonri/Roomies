import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Collocation";

export async function createCollocAsync(collocname){
    return await postAsync(endpoint, collocname);
}

export async function getCollocByRoomieIdAsync() {
    return await getAsync(endpoint);
}