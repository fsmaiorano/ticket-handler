import { Button } from '@/components/ui/button'
import { Dialog, DialogContent, DialogTrigger } from '@/components/ui/dialog'
import {
  Table,
  TableBody,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { AppContext } from '@/contexts/app-context'
import { getSectors } from '@/services/get-sectors'
import { getTickets } from '@/services/get-tickets'
import { useQuery, useQueryClient } from '@tanstack/react-query'
import { useContext } from 'react'
import { Helmet } from 'react-helmet-async'
import { CreateTicket } from './create-ticket'
import { TicketTableFilter } from './ticket-table-filter'
import { TicketTableRow } from './ticket-table-row'

export function Tickets() {
  const { user, sectorsHandler } = useContext(AppContext)
  const queryClient = useQueryClient()

  useQuery({
    queryKey: ['sectors'],
    queryFn: () =>
      getSectors({ holderId: user.holderId }).then((res) => {
        sectorsHandler(res)

        return res
      }),
    staleTime: Infinity,
  })

  const { data: result, isLoading: isLoadingTickets } = useQuery({
    queryKey: ['tickets'],
    queryFn: () =>
      getTickets({ holderId: user.holderId }).then((res) => {
        console.log(res)

        return res
      }),
    staleTime: Infinity,
  })

  const reloadTickets = () => {
    queryClient.invalidateQueries({queryKey: ['tickets']});
  }

  //   useEffect(() => {
  //     const fetchSectors = async () => {
  //       const getSectorsResponse = await getSectors({ holderId: user.holderId })
  //       sectorsHandler(getSectorsResponse)
  //     }

  //     fetchSectors()
  //   }, [user.holderId])

  return (
    <>
      <Helmet title="Tickets" />
      <div className="flex flex-col gap-4">
        <h1 className="text-3xl font-bold tracking-tight">Tickets</h1>
        <p>{user.name}</p>

        <Dialog>
          <DialogTrigger asChild>
            <Button className="absolute right-6 top-20" variant={'outline'}>
              New Ticket
            </Button>
          </DialogTrigger>
          <DialogContent>
            <CreateTicket hasNewTicket={reloadTickets} />
          </DialogContent>
        </Dialog>

        <TicketTableFilter />
        <div className="rounded-md border">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="w-[64px]"></TableHead>
                <TableHead className="">Sector</TableHead>
                <TableHead className="">Title</TableHead>
                <TableHead className="w-[120px]">Created in</TableHead>
                <TableHead className="w-[120px]">Priority</TableHead>
                <TableHead className="w-[120px]">Status</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {isLoadingTickets && <p>Loading...</p>}
              {result &&
                result.map((ticket) => {
                  return (
                    <TicketTableRow
                      key={ticket.id}
                      ticket={{
                        ...ticket,
                      }}
                    />
                  )
                })}
            </TableBody>
          </Table>
        </div>

        {/* {sectors &&
          sectors?.map((sector) => (
            <div key={sector.id} className="flex items-center justify-between">
              <div>
                <h2 className="text-xl font-bold">{sector.name}</h2>
                <p>{sector.id}</p>
              </div>
              <Button variant={'outline'}>View</Button>
            </div>
          ))} */}
      </div>
    </>
  )
}
