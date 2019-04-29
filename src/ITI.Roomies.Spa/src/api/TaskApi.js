import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Task";

export async function getTasksByCollocIdAsync(collocId) {
    return await getAsync(`${endpoint}/${collocId}`);
}