import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {

  @Output() pageChanged = new EventEmitter<number>();
  @Input() pageSize!: number
  @Input() totalCount!: number

  constructor() { }

  ngOnInit(): void {
  }

  onPagerChanged(event: any) {
    this.pageChanged.emit(event)
  }

}
