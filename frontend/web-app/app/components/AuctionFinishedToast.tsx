import { Auction, AuctionFinished } from '@/types'
import Image from 'next/image'
import Link from 'next/link'
import React from 'react'
import { numberWithCommas } from '../lib/numberWithCommas'

type Props = {
  auctionFinished: AuctionFinished
  auction: Auction
}

export default function AuctionFinishedToast({
  auction,
  auctionFinished,
}: Props) {
  return (
    <Link
      href={`/auctions/details/${auction.id}`}
      className="flex flex-col items-center"
    >
      <div className="flex flex-row items-center gap-2">
        <Image
          src={auction.imageUrl}
          alt="image"
          height={80}
          width={80}
          className="rounded-lg w-auto h-auto"
        />
        <div className="">
          <span>
            Auction for {auction.make} {auction.model} has finished
          </span>
          {auctionFinished.itemSold && auctionFinished.amount ? (
            <p>
              Congrats to {auctionFinished.winner} who has won this auction for
              $${numberWithCommas(auctionFinished.amount)}
            </p>
          ) : (
            <p>This item did not sell</p>
          )}
        </div>
      </div>
    </Link>
  )
}
