import { api } from '@/lib/axios'
import { Ticket } from '@/models/ticket'

export interface GetTicketsParams {
  holderId: string
  page?: number
  pageSize?: number
}

interface GetTicketsResponse {
  success: boolean
  message: string
  pageNumber: number
  totalPages: number
  items: Ticket[]
}

export async function getTickets({
  holderId,
  page,
  pageSize,
}: GetTicketsParams) {
  if (!page || page < 1) {
    page = 1
  }

  const response = await api.get<GetTicketsResponse>(
    `/api/ticket/holder/${holderId}?page=${page}&pageSize=${pageSize}`,
  )
  return response.data
}
