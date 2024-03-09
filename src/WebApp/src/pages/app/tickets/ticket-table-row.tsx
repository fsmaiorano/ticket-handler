import { DialogTrigger } from '@radix-ui/react-dialog'
import { formatDistanceToNow } from 'date-fns'
import { ptBR } from 'date-fns/locale'
import { Search } from 'lucide-react'
import { useContext, useEffect, useState } from 'react'

import { TicketPriority } from '@/components/ticket-priority'
import { TicketStatus } from '@/components/ticket-status'
import { Button } from '@/components/ui/button'
import { Dialog } from '@/components/ui/dialog'
import { TableCell, TableRow } from '@/components/ui/table'
import { AppContext } from '@/contexts/app-context'
import { Sector } from '@/models/sector'
import { Ticket } from '@/models/ticket'
import { TicketDetail } from './ticket-detail'

export interface TicketTableRowProps {
  ticket: Ticket
  reloadTicketList: () => void
}

export function TicketTableRow({
  ticket,
  reloadTicketList,
}: TicketTableRowProps) {
  const [isSetDetailsOpen, setDetailsOpen] = useState(false)
  const { sectors } = useContext(AppContext)
  const [selectedSector, setSelectedSector] = useState<Sector>()

  useEffect(() => {
    if (ticket.sectorId) {
      const sector = sectors.find((s) => s.id === ticket.sectorId)
      setSelectedSector(sector)
    }
  }, [ticket.sectorId, sectors])

  const handleRefreshTickets = () => {
    reloadTicketList()
  }

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
          <TicketDetail
            ticket={ticket}
            hasUpdateTicket={handleRefreshTickets}
          />
        </Dialog>
      </TableCell>
      <TableCell className="font-mono text-xs font-medium">
        {selectedSector?.name || 'N/A'}
      </TableCell>
      <TableCell className="font-mono text-xs font-medium">
        {ticket.title}
      </TableCell>
      <TableCell className="text-muted-foreground">
        {formatDistanceToNow(ticket.createdAt, {
          locale: ptBR,
          addSuffix: true,
        })}
      </TableCell>
      <TableCell className="font-mono text-xs font-medium">
        <TicketPriority priority={ticket.priority} />
      </TableCell>
      <TableCell className="font-mono text-xs font-medium">
        <TicketStatus status={ticket.status} />
      </TableCell>
    </TableRow>
  )
}
