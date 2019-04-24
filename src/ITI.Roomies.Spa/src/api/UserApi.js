import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/User";

export async function getUsersByIdAsync(UserId) {
    return await getAsync(endpoint, UserId);
}