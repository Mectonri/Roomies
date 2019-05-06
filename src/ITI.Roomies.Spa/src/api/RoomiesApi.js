import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Roomies";

export async function getRoomieByIdAsync(roomieId) {
    return await getAsync(`${endpoint}/checkRoomie/${roomieId}`);
}
export async function FindByEmail() {
    return await getAsync(`${endpoint}/getRoomie`);
}

export async function createRoomieAsync(model) {
    return await postAsync(`${endpoint}/createRoomie`, model);
}

export async function inviteRoomieAsync(email){
    return await postAsync((`${endpoint}/${email}/invite`));
}

export async function uploadImage( image ){
    return await postAsync((`${endpoint}/${image}`));
}

