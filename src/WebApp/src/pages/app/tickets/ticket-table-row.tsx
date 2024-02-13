import { DialogTrigger } from '@radix-ui/react-dialog'
import { formatDistanceToNow } from 'date-fns'
import { ptBR } from 'date-fns/locale'
import { ArrowRight, Search, X } from 'lucide-react'
import { useState } from 'react'

import { Button } from '@/components/ui/button'
import { Dialog } from '@/components/ui/dialog'
import { TableCell, TableRow } from '@/components/ui/table'
import { Ticket } from '@/models/ticket'
import { TicketStatus } from '@/models/ticket-status'

export interface TicketTableRowProps {
  ticket: Ticket
}

export function TicketTableRow({ ticket }: TicketTableRowProps) {
  const [isSetDetailsOpen, setDetailsOpen] = useState(false)
  //   const queryClient = useQueryClient()

  //   function updateTicketStatusOnCache(ticketId: string, status: TicketStatus) {
  //     const ticketsListCache = queryClient.getQueriesData<GetTicketsResponse>({
  //       queryKey: ['tickets'],
  //     })

  //     ticketsListCache.forEach(([cacheKey, cacheData]) => {
  //       if (!cacheData) {
  //         return
  //       }

  //       queryClient.setQueryData<GetTicketsResponse>(cacheKey, {
  //         ...cacheData,
  //         tickets: cacheData.tickets.map((ticket) => {
  //           if (ticket.ticketId === ticketId) {
  //             return { ...ticket, status }
  //           }

  //           return ticket
  //         }),
  //       })
  //     })
  //   }

  //   const { mutateAsync: cancelTicketFn, isPending: isCancelingTicket } =
  //     useMutation({
  //       mutationFn: cancelTicket,
  //       async onSuccess(_, { ticketId }) {
  //         updateTicketStatusOnCache(ticketId, 'canceled')
  //       },
  //     })

  //   const { mutateAsync: approveTicketFn, isPending: isApprovingTicket } =
  //     useMutation({
  //       mutationFn: approveTicket,
  //       async onSuccess(_, { ticketId }) {
  //         updateTicketStatusOnCache(ticketId, 'processing')
  //       },
  //     })

  //   const { mutateAsync: dispatchTicketFn, isPending: isDispatchingTicket } =
  //     useMutation({
  //       mutationFn: dispatchTicket,
  //       async onSuccess(_, { ticketId }) {
  //         updateTicketStatusOnCache(ticketId, 'delivering')
  //       },
  //     })

  //   const { mutateAsync: deliverTicketFn, isPending: isDeliveringTicket } =
  //     useMutation({
  //       mutationFn: deliverTicket,
  //       async onSuccess(_, { ticketId }) {
  //         updateTicketStatusOnCache(ticketId, 'delivered')
  //       },
  //     })

  return (
    <TableRow>
      <TableCell>
        <Dialog open={isSetDetailsOpen} onOpenChange={setDetailsOpen}>
          <DialogTrigger asChild>
            <Button variant="outline" size="xs">
              <Search className="h-3 w-3" />
              <span className="sr-only">Ticket detail</span>
            </Button>
          </DialogTrigger>
          {/* <TicketDetails ticketId={ticket.ticketId} open={isSetDetailsOpen} /> */}
        </Dialog>
      </TableCell>
      <TableCell className="font-mono text-xs font-medium">
        {ticket.id}
      </TableCell>
      <TableCell className="text-muted-foreground">
        {formatDistanceToNow(ticket.createdAt, {
          locale: ptBR,
          addSuffix: true,
        })}
      </TableCell>
      <TableCell>
        {/* <TicketStatus status={ticket.status} /> */}
        Ticket status"
      </TableCell>
      <TableCell className="font-medium">{ticket.title}</TableCell>
      <TableCell>
        {ticket.status === TicketStatus.Open && (
          <Button
            variant="outline"
            // disabled={isApprovingTicket}
            size="xs"
            // onClick={() => approveTicketFn({ ticketId: ticket.ticketId })}
          >
            <ArrowRight className="mr-2 h-3 w-3" />
            Aprovar
          </Button>
        )}

        {ticket.status === TicketStatus.Active && (
          <Button
            variant="outline"
            // disabled={isDispatchingTicket}
            size="xs"
            // onClick={() => dispatchTicketFn({ ticketId: ticket.ticketId })}
          >
            <ArrowRight className="mr-2 h-3 w-3" />
            Em entrega
          </Button>
        )}

        {ticket.status === TicketStatus.Closed && (
          <Button
            variant="outline"
            // disabled={isDeliveringTicket}
            size="xs"
            // onClick={() => deliverTicketFn({ ticketId: ticket.ticketId })}
          >
            <ArrowRight className="mr-2 h-3 w-3" />
            Entregue
          </Button>
        )}
      </TableCell>
      <TableCell>
        <Button
          variant="ghost"
          size="xs"
          //   disabled={
          //     !['pending', 'processing'].includes(ticket.status) ||
          //     isCancelingTicket
          //   }
          //   onClick={() => cancelTicketFn({ ticketId: ticket.ticketId })}
        >
          <X className="mr-2 h-3 w-3" />
          Cancel
        </Button>
      </TableCell>
    </TableRow>
  )
}
