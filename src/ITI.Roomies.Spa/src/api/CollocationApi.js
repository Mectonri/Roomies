import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Collocation";

export async function createCollocAsync(collocname){
    return await postAsync(endpoint, collocname);
}

export async function getCollocNameIdByRoomieIdAsync() {
    return await getAsync(`${endpoint}/getNameId`);
}

export async function GetRoomiesIdNamesByCollocIdAsync(collocId) {
    return await getAsync(`${endpoint}/getRoomieIdNames/${collocId}`);
};

export async function quitCollocAsync(collocId){
    return await deleteAsync(`${endpoint}/quitColloc/${collocId}`);
}