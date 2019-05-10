import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import { async } from 'q';

const endpoint = process.env.VUE_APP_BACKEND + "/api/item";

///get the list of items of a specific grocery list
export async function getItemListAsync(courseId) {
  return await getAsync(`${endpoint}/${courseId}`);
}
export async function createItemAsync(model) {
  return await postAsync(endpoint, model);
}


export async function getAllAsync(){
  return await getAsync(`${endpoint}`);
}

export async function updateItemAsync(model){
  return await putAsync(`${endpoint}/${model.itemId}`, model);
}

export async function deleteItemAsync(itemId){
  return await deleteAsync(`${endpoint}/${itemId}`);
}