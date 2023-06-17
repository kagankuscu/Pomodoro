//
//  CouterDownViewModel.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 10.06.23.
//

import Foundation
import SwiftUI

class CounterDownViewModel: ObservableObject {
    @Published var isActive: Bool = false
    @Published var time: String = "0:00"
    @Published var breakName: String = ""
    @Published var countSet: Int = 0
    @Published var isCompleted: Bool = false
    @Published var minutes: Double = 25.0 {
        didSet{
            time = "\(Int(minutes)):00"
        }
    }
    @Published var isShare = false
    
    private var initialTime = 0
    private var endDate = Date()
    private var isBreak = false
    private var pomodoro = [25.0, 5.0, 15.0]
    
    func start(minutes: Double) {
        self.initialTime = Int(minutes)
        self.minutes = minutes
        self.breakName = "Work"
        self.isActive = true
        self.endDate = Date()
        self.endDate = Calendar.current.date(byAdding: .minute, value: Int(minutes), to: endDate)!
        
    }
    
    func skip() {
        isBreak.toggle()
        
        if isBreak {
            if self.countSet < 4 {
//                self.minutes = pomodoro[1]
                self.start(minutes: pomodoro[1])
                self.breakName = "Short Break"
            } else {
//                self.minutes = pomodoro[2]
                self.start(minutes: pomodoro[2])
                self.breakName = "Long Break"
            }
            
        } else {
//            self.minutes = pomodoro[0]
            self.start(minutes: pomodoro[0])
            self.breakName = "Work"
        }
        
        if !self.isBreak {
            self.countSet += 1
        }
        
        if self.countSet > 4 {
            self.countSet = 0
            self.isCompleted = true
        }
    }
    
    func reset() {
        self.minutes = 0.0
        self.countSet = 0
        self.breakName = ""
        self.isActive = false
        self.isCompleted = false
    }
    
    func updateCountDown() {
        guard isActive else { return }
        
        let now = Date()
        let diff = endDate.timeIntervalSince1970 - now.timeIntervalSince1970
        
        if diff <= 0 {
            self.skip()
        }
        let date = Date(timeIntervalSince1970: diff)
        let calendar = Calendar.current
        let minutes = calendar.component(.minute, from: date)
        let seconds = calendar.component(.second, from: date)
        
        self.minutes = Double(minutes)
        self.time = String(format: "%d:%02d", minutes, seconds)
    }
}
