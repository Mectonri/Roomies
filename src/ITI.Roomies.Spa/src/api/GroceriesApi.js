import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = process.env.VUE_APP_BACKEND + "/api/Groceries";


export async function getGroceryListByIdAsync(courseId) {
  return await getAsync(`${endpoint}/${courseId}`);
}

export async function createGroceryListAsync(model) {
  return await postAsync(endpoint, model);
}
