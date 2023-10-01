import { Button } from 'flowbite-react';
import React from 'react';
import { useParamsStore } from '../hooks/useParamStore';

const pageZiseButtons = [4, 8, 12];

export default function Filters() {
  const pageSize = useParamsStore(state => state.pageSize);
  const setParams = useParamsStore(state => state.setParams);
  return (
    <div className='flex justify-between items-center mb-4'>
      <div>
        <span className='uppercase text-sm text-gray-500 mr-2'>Page Size</span>
        <Button.Group>
          {pageZiseButtons.map((value, i) => (
            <Button
              key={i}
              onClick={() => setParams({ pageSize: value })}
              color={`${pageSize === value ? 'red' : 'gray'}`}>
              {' '}
              {value}
            </Button>
          ))}
        </Button.Group>
      </div>
    </div>
  );
}
