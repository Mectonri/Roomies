import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Task";

export async function getTasksByCollocIdAsync(collocId) {
    return await getAsync(`${endpoint}/getByCollocId/${collocId}`);
}

export async function GetTasksByRoomieIdAsync() {
    return await getAsync(`${endpoint}/getByRoomieId`);
}

export async function createTaskSansDescAsync(model) {
    return await postAsync(`${endpoint}/createTaskSansDesc`, model);
}

export async function UpdateTaskStateAsync(taskId, taskState) {
    return await postAsync(`${endpoint}/updateTaskState/${taskId}/${taskState}`);
}

export async function DeleteTaskByIdAsync(taskId) {
    return await deleteAsync(`${endpoint}/deleteTask/${taskId}`);
}