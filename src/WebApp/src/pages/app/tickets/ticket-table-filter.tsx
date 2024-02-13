import { zodResolver } from '@hookform/resolvers/zod'
import { Search, X } from 'lucide-react'
import { Controller, useForm } from 'react-hook-form'
import { useSearchParams } from 'react-router-dom'
import { z } from 'zod'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from '@/components/ui/select'

import { TicketPriority } from '@/models/ticket-priority'

const orderFilterSchema = z.object({
  ticketId: z.string().optional(),
  customerName: z.string().optional(),
  status: z.string().optional(),
})

type OrderFilterSchema = z.infer<typeof orderFilterSchema>

export function TicketTableFilter() {
  const [searchParams, setSearchParams] = useSearchParams()
  const ticketId = searchParams.get('ticketId')
  const customerName = searchParams.get('customerName')
  const status = searchParams.get('status')

  const { register, handleSubmit, control, reset } = useForm<OrderFilterSchema>(
    {
      resolver: zodResolver(orderFilterSchema),
      defaultValues: {
        ticketId: ticketId ?? '',
        customerName: customerName ?? '',
        status: status ?? 'all',
      },
    },
  )

  function handleFilter({ ticketId, customerName, status }: OrderFilterSchema) {
    setSearchParams((state) => {
      if (ticketId) {
        state.set('ticketId', ticketId.trim())
      } else {
        state.delete('ticketId')
      }

      if (customerName) {
        state.set('customerName', customerName.trim())
      } else {
        state.delete('customerName')
      }

      if (status) {
        state.set('status', status.trim())
      } else {
        state.delete('status')
      }

      state.set('page', '1')
      return state
    })
  }

  function handleClearFilters() {
    setSearchParams((state) => {
      state.delete('ticketId')
      state.delete('customerName')
      state.delete('status')
      state.set('page', '1')
      return state
    })

    reset({
      ticketId: '',
      customerName: '',
      status: 'all',
    })
  }

  return (
    <>
      <form
        onSubmit={handleSubmit(handleFilter)}
        className="flex items-center gap-2"
      >
        <span className="text-sm font-semibold">Filters:</span>
        <Input
          className="h-8 w-auto"
          placeholder="Ticket ID"
          {...register('ticketId')}
        />
        <Input
          className="h-8 w-[320px]"
          placeholder="Client name"
          {...register('customerName')}
        />
        <Controller
          name="status"
          control={control}
          render={({ field: { name, onChange, value, disabled } }) => {
            return (
              <Select
                defaultValue="all"
                name={name}
                onValueChange={onChange}
                value={value}
                disabled={disabled}
              >
                <SelectTrigger className="h-8 w-[180px]">
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="all">All</SelectItem>
                  {Object.keys(TicketPriority)
                    .filter((value) => isNaN(Number(value)))
                    .map((priority) => {
                      return (
                        <SelectItem
                          key={priority.toLocaleLowerCase()}
                          value={priority}
                        >
                          {priority}
                        </SelectItem>
                      )
                    })}
                </SelectContent>
              </Select>
            )
          }}
        ></Controller>
        <Button type="submit" variant="secondary" size="xs">
          <Search className="mr-2 h-4 w-4" />
          Filter results
        </Button>
        <Button
          onClick={handleClearFilters}
          type="button"
          variant="outline"
          size="xs"
        >
          <X className="mr-2 h-4 w-4" />
          Remove filters
        </Button>
      </form>
    </>
  )
}
