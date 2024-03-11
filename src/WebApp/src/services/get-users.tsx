import { api } from '@/lib/axios'

interface GetUsersRequest {
  holderId: string
}

// interface GetUsersResponse {
// }

export async function getUsers({ holderId }: GetUsersRequest) {
  const url = `/api/holder/${holderId}/users`
  const response = await api.get(url.trim())

  return response.data
}
